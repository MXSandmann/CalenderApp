using ApplicationCore.Models.Entities;

namespace ApplicationCore.Services.Contracts
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<Subscription>> GetSubscriptions();
        Task<Subscription> CreateSubscription(Subscription subscription);
        Task RemoveSubscription(Guid id);
        Task<Subscription> GetSubscriptionById(Guid id);
        Task<Subscription> UpdateSubscription(Subscription subscription);
    }
}
