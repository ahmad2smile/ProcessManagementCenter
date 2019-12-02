using System.Threading.Tasks;
using Notifications.Domain;

namespace Notifications.Context.Commands
{
    public interface IPublishNotificationCommand
    {
        Task<bool> Handler(Notification notification);
    }
}