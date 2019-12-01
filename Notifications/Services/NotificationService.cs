using Microsoft.Azure.NotificationHubs;
using Microsoft.Extensions.Configuration;
using Notifications.Domain;
using System;
using System.Threading.Tasks;
using Notification = Notifications.Domain.Notification;

namespace Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationHubClient _hub;

        public NotificationService(IConfiguration configuration)
        {
            var hubName = configuration["HubName"];
            var hubConnectionString = configuration["HubConnectionString"];

            _hub = new NotificationHubClient(hubConnectionString, hubName);
        }

        public async Task<bool> SendToAll(Notification notification, Platform platform)
        {
            NotificationOutcome result;

            string payload;

            switch (platform)
            {
                case Platform.Android:
                    payload =
                        $@"<toast><visual><binding template=""ToastText01""><text id=""1"">{notification.NotificationType.Name}</text></binding></visual></toast>";
                    result = await _hub.SendFcmNativeNotificationAsync(payload);
                    break;

                case Platform.Iphone:
                    payload = $"{{\"aps\":{{\"alert\":\": {notification.NotificationType.Name}\"}}}}";
                    result = await _hub.SendAppleNativeNotificationAsync(payload);
                    break;

                case Platform.Web:
                    payload = $"{{ \"data\" : {{\"message\":\"{notification.NotificationType.Name}\"}}}}";
                    result = await _hub.SendFcmNativeNotificationAsync(payload); //TODO: Replace with Self Managed Web Notifications
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(platform), platform, "Invalid platform option");
            }

            return !(result.State == NotificationOutcomeState.Abandoned ||
                     result.State == NotificationOutcomeState.Unknown);
        }
    }
}