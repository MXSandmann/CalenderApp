using WebUI.Models.Dtos;

namespace WebUI.Clients.Contracts
{
    public interface ISubscriptionsClient
    {
        Task<SubscriptionDto> GetSubscriptionById(Guid id);
        Task<SubscriptionDto> AddSubscription(SubscriptionDto subscriptionDto);                
        Task<SubscriptionDto> UpdateSubscription(SubscriptionDto subscriptionDto);
        Task RemoveSubscription(Guid id);
        Task<NotificationDto> AddNotification(NotificationDto notificationDto);
        Task<IEnumerable<SubscriptionDto>> GetAllSubscriptions();
    }
}
