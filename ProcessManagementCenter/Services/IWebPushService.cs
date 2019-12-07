using ProcessManagementCenter.Domain;
using System.Threading.Tasks;

namespace ProcessManagementCenter.Services
{
    public interface IWebPushService
    {
        Task SendNotification(Subscription subscription, Notification notification);
    }
}