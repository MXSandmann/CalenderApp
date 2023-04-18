namespace WebUI.Models.ViewModels
{
    public class CreateNotificationViewModel
    {
        public Guid SubscriptionId { get; set; }
        public TimeSpan NotificationTimeSpan { get; set; }
    }
}
