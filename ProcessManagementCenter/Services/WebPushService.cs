using Microsoft.Extensions.Configuration;
using ProcessManagementCenter.Domain;
using ProcessManagementCenter.Utils;
using System.Threading.Tasks;
using WebPush;

namespace ProcessManagementCenter.Services
{
    public class WebPushService : IWebPushService
    {
        private readonly WebPushClient _webPushClient;
        private readonly VapidDetails _vapidDetails;

        public WebPushService(IConfiguration configuration)
        {
            _webPushClient = new WebPushClient();
            var vapidPublicKey = configuration["VAPID:public"];
            var vapidPrivateKey = configuration["VAPID:private"];

            _vapidDetails = new VapidDetails("mailto:ahmad@sennalabs.com", vapidPublicKey, vapidPrivateKey);
        }

        public Task SendNotification(Subscription subscription, Notification notification)
        {
            var pushSubscription = new PushSubscription(subscription.PushEndpoint, subscription.PushP256Dh, subscription.PushAuth);

            return _webPushClient.SendNotificationAsync(pushSubscription, Serializer.ToJsonString(notification), _vapidDetails);
        }
    }
}
