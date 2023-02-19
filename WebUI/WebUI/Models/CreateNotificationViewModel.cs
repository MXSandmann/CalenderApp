using WebUI.Models.Enums;

namespace WebUI.Models
{
    public class CreateNotificationViewModel
    {
        public Guid SubscriptionId { get; set; }
        public NotificationTime NotificationTime { get; set; }
    }
}
