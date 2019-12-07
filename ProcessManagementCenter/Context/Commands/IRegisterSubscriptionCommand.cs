using ProcessManagementCenter.Domain;
using System.Threading.Tasks;

namespace ProcessManagementCenter.Context.Commands
{
    public interface IRegisterSubscriptionCommand
    {
        Task<bool> Handler(Subscription subscription);
    }
}