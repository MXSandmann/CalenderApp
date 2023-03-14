using ApplicationCore.Models.Entities;
using ApplicationCore.Repositories.Contracts;
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

        public async Task RemoveSubscription(Guid id)
        {
            var subscriptionToDelete = await _subscriptionRepository.GetById(id);
            ArgumentNullException.ThrowIfNull(subscriptionToDelete);
            await _subscriptionRepository.Remove(subscriptionToDelete);
        }

        public async Task<Subscription> UpdateSubscription(Subscription subscription)
        {
            var subscriptionUpdated = await _subscriptionRepository.Update(subscription);
            return subscriptionUpdated;
        }
    }
}
