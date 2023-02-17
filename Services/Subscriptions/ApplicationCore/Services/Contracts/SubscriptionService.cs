using ApplicationCore.Models.Entities;
using ApplicationCore.Repositories;

namespace ApplicationCore.Services.Contracts
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<Subscription> CreateSubscription(Subscription subscription)
        {
            var newSubscription = await _subscriptionRepository.Add(subscription);
            return newSubscription;
        }

        public async Task<Subscription> GetSubscriptionById(Guid id)
        {
            return await _subscriptionRepository.GetById(id);
        }

        public async Task<IEnumerable<Subscription>> GetSubscriptions()
        {
            return await _subscriptionRepository.GetAll();
        }

        public Task RemoveSubscription(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Subscription> UpdateSubscription(Subscription subscription)
        {
            throw new NotImplementedException();
        }
    }
}
