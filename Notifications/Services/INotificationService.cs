using System.Threading.Tasks;
using Notifications.Domain;

namespace Notifications.Services
{
    public interface INotificationService
    {
        Task<bool> SendToAll(Notification notification, Platform platform);
    }
}