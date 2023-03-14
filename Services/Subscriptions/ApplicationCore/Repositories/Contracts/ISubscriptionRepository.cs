using ApplicationCore.Models.Entities;

namespace ApplicationCore.Repositories.Contracts
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetAll();
        Task<Subscription> GetById(Guid id);
        Task SaveAsync();
        Task Remove(Subscription subscription);
        Task<Subscription> Update(Subscription subscription);
        Task<Subscription> Add(Subscription subscription);
    }
}
