using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Notifications.Domain;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Context.Commands
{
    public class PublishNotificationCommand : IPublishNotificationCommand
    {
        private readonly ILogger<PublishNotificationCommand> _logger;
        private readonly HttpClient _client;

        public PublishNotificationCommand(IConfiguration configuration, ILogger<PublishNotificationCommand> logger)
        {
            _logger = logger;
            _client = new HttpClient();

            var apiGateway = configuration["Services:ApiGateway"];

            _client.BaseAddress = new Uri(apiGateway);
        }

        public async Task<bool> Handler(Notification notification)
        {
            try
            {
                var result = await _client.PostAsync("/api/Notifications",
                    new StringContent(
                        notification.ToString(),
                        Encoding.UTF8,
                        "application/json"));

                _logger.LogDebug($"Published Notification Id {notification.Id}, with Status: {result.StatusCode}");

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error Publishing Notification Id {notification.Id}, with Error: {exception.Message}");

                return false;
            }
        }
    }
}
