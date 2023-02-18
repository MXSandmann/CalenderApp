using WebUI.Models.Dtos;

namespace WebUI.Clients.Contracts
{
    public interface ISubscriptionsClient
    {
        Task<IEnumerable<SubscriptionDto>> GetSubscriptionForEvent(Guid eventId);
        Task<SubscriptionDto> AddSubscription(SubscriptionDto subscriptionDto);                
        Task<SubscriptionDto> UpdateSubscription(SubscriptionDto subscriptionDto);
        Task SubscriptionForEvent(Guid eventId);
        Task<NotificationDto> AddNotification(NotificationDto notificationDto);
    }
}
