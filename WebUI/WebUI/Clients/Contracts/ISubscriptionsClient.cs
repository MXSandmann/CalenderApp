using WebUI.Models.Dtos;

namespace WebUI.Clients.Contracts
{
    public interface ISubscriptionsClient
    {
        Task<IEnumerable<SubscriptionDto>> GetSubscriptionForEvent(Guid eventId);
        Task<SubscriptionDto> AddSubscription(SubscriptionDto subscriptionDto);                
        Task<SubscriptionDto> UpdateSubscription(SubscriptionDto subscriptionDto);
        Task RemoveSubscriptionForEvent(Guid eventId);
        Task<NotificationDto> AddNotification(NotificationDto notificationDto);
        Task<IEnumerable<SubscriptionDto>> GetAllSubscriptions();
    }
}
