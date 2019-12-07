using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProcessManagementCenter.Context.Commands;
using ProcessManagementCenter.Domain;
using ProcessManagementCenter.Services;
using System;
using System.Threading.Tasks;

namespace ProcessManagementCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IHubContext<ApplicationHub> _hubContext;
        private readonly IConfiguration _configuration;
        private readonly IWebPushService _webPushService;
        private readonly IRegisterSubscriptionCommand _registerSubscriptionCommand;
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(IHubContext<ApplicationHub> hubContext,
            IConfiguration configuration,
            IWebPushService webPushService,
            IRegisterSubscriptionCommand registerSubscriptionCommand,
            ILogger<NotificationsController> logger)
        {
            _hubContext = hubContext;
            _configuration = configuration;
            _webPushService = webPushService;
            _registerSubscriptionCommand = registerSubscriptionCommand;
            _logger = logger;
        }

        [HttpGet("vapid-key")]
        public ActionResult<string> GetPublicVapidKey()
        {
            return Ok(_configuration["VAPID:public"]);
        }

        [HttpPost("subscription")]
        public async Task<ActionResult<Notification>> PostSubscription(Subscription subscription)
        {
            try
            {
                await _registerSubscriptionCommand.Handler(subscription);

                _logger.LogDebug("Subscribing for subscription," +
                                 $"id: {subscription.Id}," +
                                 $"name: {subscription.Name}," +
                                 $"endpoint: {subscription.PushEndpoint}");

                var notification = new Notification
                {
                    NotificationStatus = NotificationStatus.InActive,

                    NotificationType = new NotificationType
                    {
                        Code = "SUBSCRIPTION",
                        Name = "Successfully subscribed to the notifications"
                    }
                };

                await _webPushService.SendNotification(subscription, notification);

                return Ok(notification);
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception: {exception}", exception);

                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        {
            await _hubContext.Clients.Group(notification.MineArea.Code).SendAsync("Notification", notification);

            return Ok(notification);
        }
    }
}
