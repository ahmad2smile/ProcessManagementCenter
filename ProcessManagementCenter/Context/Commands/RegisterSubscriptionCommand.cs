using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProcessManagementCenter.Domain;
using ProcessManagementCenter.Utils;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProcessManagementCenter.Context.Commands
{
    public class RegisterSubscriptionCommand : IRegisterSubscriptionCommand
    {
        private readonly ILogger<RegisterSubscriptionCommand> _logger;
        private readonly HttpClient _client;

        public RegisterSubscriptionCommand(IConfiguration configuration, ILogger<RegisterSubscriptionCommand> logger)
        {
            _logger = logger;
            _client = new HttpClient();

            var notificationService = configuration["Services:Notifications"];

            _client.BaseAddress = new Uri(notificationService);
        }

        public async Task<bool> Handler(Subscription subscription)
        {
            try
            {
                var result = await _client.PostAsync("/api/Subscriptions", Serializer.ToStringContent(subscription));

                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception(result.Content.ToString());
                }

                _logger.LogDebug($"Subscribed Device {subscription.DeviceId}, with Status: {result.StatusCode}");

                return true;
            }
            catch (Exception exception)
            {
                var errorMessage = $"Error Subscribing the Device {subscription.DeviceId}, with Error: {exception.Message}, on Service: {_client.BaseAddress}";
                _logger.LogError(errorMessage);

                throw new Exception(errorMessage);
            }
        }
    }
}
