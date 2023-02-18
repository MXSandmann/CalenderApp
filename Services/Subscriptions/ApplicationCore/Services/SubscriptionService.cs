using ApplicationCore.Models.Entities;
using ApplicationCore.Repositories;
using ApplicationCore.Services.Contracts;

namespace ApplicationCore.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly INotificationRepository _notificationRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, INotificationRepository notificationRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<Notification> AddNotification(Notification notification)
        {
            var newNotification = await _notificationRepository.Add(notification);
            return newNotification;
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
