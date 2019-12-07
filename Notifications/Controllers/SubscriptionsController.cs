using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Notifications.Context;
using Notifications.Domain;
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

        public SubscriptionsController(NotificationContext context, ILogger<SubscriptionsController> logger)
        {
            _context = context;
            _logger = logger;
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
        public async Task<ActionResult<Subscription>> PostSubscription([Bind("Id,Name,PushEndpoint,PushP256DH,PushAuth")] Subscription subscription)
        {
            try
            {
                _context.Subscriptions.Add(subscription);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError("Error while trying to add new Subscription {exception}", exception);

                return BadRequest("Unable to register subscription");
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
