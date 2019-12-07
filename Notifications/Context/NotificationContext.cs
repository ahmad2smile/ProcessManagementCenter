using Microsoft.EntityFrameworkCore;
using Notifications.Domain;

namespace Notifications.Context
{
    public class NotificationContext : DbContext
    {
        public NotificationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
    }
}
