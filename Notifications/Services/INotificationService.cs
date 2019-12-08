using Notifications.Context;
using Notifications.Domain;
using System.Threading.Tasks;

namespace Notifications.Services
{
    public interface INotificationService
    {
        Task<bool> SendToSubscription(Notification notification, Subscription subscription);
        Task<bool> SendToAll(Notification notification, NotificationContext context);
        Task<bool> SendToPlatform(Notification notification, Platform platform, NotificationContext context);
    }
}