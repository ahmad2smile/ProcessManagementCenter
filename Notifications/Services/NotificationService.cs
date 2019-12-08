using Microsoft.Azure.NotificationHubs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Notifications.Context;
using Notifications.Domain;
using Notifications.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebPush;
using Notification = Notifications.Domain.Notification;

namespace Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly NotificationHubClient _hub;
        private readonly VapidDetails _vapidDetails;
        private readonly WebPushClient _webPushClient;

        public NotificationService(IConfiguration configuration, ILogger<NotificationService> logger)
        {
            _logger = logger;
            var hubName = configuration["ANH:HubName"];
            var hubConnectionString = configuration["ANH:HubConnectionString"];

            _hub = new NotificationHubClient(hubConnectionString, hubName);

            _webPushClient = new WebPushClient();
            var vapidPublicKey = configuration["VAPID:public"];
            var vapidPrivateKey = configuration["VAPID:private"];

            _vapidDetails = new VapidDetails("mailto:ahmad@sennalabs.com", vapidPublicKey, vapidPrivateKey);
        }

        public async Task<bool> SendToSubscription(Notification notification, Subscription subscription)
        {
            var pushSubscription = new PushSubscription(subscription.PushEndpoint, subscription.PushP256Dh, subscription.PushAuth);
            try
            {
                await _webPushClient.SendNotificationAsync(pushSubscription,
                    Serializer.ToJsonString(notification), _vapidDetails);

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    $"Error sending web notification to Device: {subscription.DeviceId} with Error: ${exception.Message}");

                return false;
            }
        }

        public async Task<bool> SendToAll(Notification notification, NotificationContext context)
        {
            var finalResult = true;

            try
            {
                var payload =
                    $@"<toast><visual><binding template=""ToastText01""><text id=""1"">{notification.NotificationType.Name}</text></binding></visual></toast>";
                var result = await _hub.SendFcmNativeNotificationAsync(payload);

                if (result.State == NotificationOutcomeState.Abandoned || result.State == NotificationOutcomeState.Unknown)
                {
                    finalResult = false;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    $"Error sending Android notification with Error: ${exception.Message}");

                finalResult = false;
            }

            try
            {
                var payload = $"{{\"aps\":{{\"alert\":\": {notification.NotificationType.Name}\"}}}}";
                var result = await _hub.SendAppleNativeNotificationAsync(payload);
                if (result.State == NotificationOutcomeState.Abandoned || result.State == NotificationOutcomeState.Unknown)
                {
                    finalResult = false;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    $"Error sending iOS notification with Error: ${exception.Message}");
                finalResult = false;
            }

            var relevantSubscriptions = context.Subscriptions.Where(s => s.MineSiteId == notification.MineSiteId).ToList();
            var webResultTasks = relevantSubscriptions
                .Select(async subscription => await SendToSubscription(notification, subscription));

            var webResults = await Task.WhenAll(webResultTasks);

            if (!webResults.All(webRes => webRes))
            {
                finalResult = false;
            }

            return finalResult;
        }

        public async Task<bool> SendToPlatform(Notification notification, Platform platform, NotificationContext context)
        {
            NotificationOutcome result;

            string payload;

            switch (platform)
            {
                case Platform.Android:
                    payload =
                        $@"<toast><visual><binding template=""ToastText01""><text id=""1"">{notification.NotificationType.Name}</text></binding></visual></toast>";
                    result = await _hub.SendFcmNativeNotificationAsync(payload);
                    return !(result.State == NotificationOutcomeState.Abandoned ||
                             result.State == NotificationOutcomeState.Unknown);

                case Platform.Iphone:
                    payload = $"{{\"aps\":{{\"alert\":\": {notification.NotificationType.Name}\"}}}}";
                    result = await _hub.SendAppleNativeNotificationAsync(payload);
                    return !(result.State == NotificationOutcomeState.Abandoned ||
                             result.State == NotificationOutcomeState.Unknown);

                case Platform.Web:
                    var relevantSubscriptions = context.Subscriptions.Where(s => s.MineSiteId == notification.MineSiteId).ToList();
                    var webResultTasks = relevantSubscriptions
                        .Select(async s =>
                        {
                            var pushSubscription = new PushSubscription(s.PushEndpoint, s.PushP256Dh, s.PushAuth);
                            try
                            {
                                await _webPushClient.SendNotificationAsync(pushSubscription,
                                    Serializer.ToJsonString(notification), _vapidDetails);

                                return true;
                            }
                            catch (Exception exception)
                            {
                                _logger.LogError(
                                    $"Error sending web notification to Device: {s.DeviceId} with Error: ${exception.Message}");

                                return false;
                            }
                        });
                    var webResults = await Task.WhenAll(webResultTasks);

                    return webResults.All(webRes => webRes);
                default:
                    throw new ArgumentOutOfRangeException(nameof(platform), platform, "Invalid platform option");
            }
        }
    }
}