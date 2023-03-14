namespace WebUI.Models
{
    public class CreateNotificationViewModel
    {
        public Guid SubscriptionId { get; set; }
        public TimeSpan NotificationTimeSpan { get; set; }
    }
}
