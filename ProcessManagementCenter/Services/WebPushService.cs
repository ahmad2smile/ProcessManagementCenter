using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProcessManagementCenter.Domain;
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

            var contractResolver = new DefaultContractResolver // TODO: Move out serialize logic to utils
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var payload = JsonConvert.SerializeObject(notification, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });

            return _webPushClient.SendNotificationAsync(pushSubscription, payload, _vapidDetails);
        }
    }
}
