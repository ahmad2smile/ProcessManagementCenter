using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Notifications.Context;
using Notifications.Domain;
using Notifications.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly NotificationContext _context;
        private readonly ILogger<SubscriptionsController> _logger;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;

        public SubscriptionsController(NotificationContext context,
            ILogger<SubscriptionsController> logger,
            INotificationService notificationService,
            IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _notificationService = notificationService;
            _configuration = configuration;
        }

        [HttpGet("vapid-key")]
        public ActionResult<string> GetPublicVapidKey()
        {
            return Ok(_configuration["VAPID:public"]);
        }

        // GET: api/Subscriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscription>>> GetSubscriptions()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        // GET: api/Subscriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscription>> GetSubscription(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);

            if (subscription == null)
            {
                return NotFound();
            }

            return subscription;
        }

        // PUT: api/Subscriptions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscription(int id, Subscription subscription)
        {
            if (id != subscription.Id)
            {
                return BadRequest();
            }

            _context.Entry(subscription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Subscriptions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Subscription>> PostSubscription([Bind("Id,MineSiteId,DeviceId,PushEndpoint,PushP256Dh,PushAuth")] Subscription subscription)
        {
            try
            {
                var existingSubscription = await _context.Subscriptions.FirstOrDefaultAsync(s => s.DeviceId == subscription.DeviceId);

                if (existingSubscription != null)
                {
                    await PutSubscription(existingSubscription.Id, subscription);

                    return Ok();
                }

                _context.Subscriptions.Add(subscription);
                await _context.SaveChangesAsync();

                var notification = new Notification
                {
                    NotificationStatus = NotificationStatus.Active,

                    NotificationType = new NotificationType
                    {
                        Code = "SUBSCRIPTION",
                        Name = "Successfully subscribed to the notifications"
                    }
                };

                await _notificationService.SendToSubscription(notification, subscription);

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError("Error while trying to add new Subscription {exception}", exception);

                return BadRequest(exception.Message);
            }
        }

        // DELETE: api/Subscriptions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Subscription>> DeleteSubscription(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();

            return subscription;
        }

        private bool SubscriptionExists(int id)
        {
            return _context.Subscriptions.Any(e => e.Id == id);
        }
    }
}
