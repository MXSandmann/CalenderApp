using WebUI.Models.Enums;

namespace WebUI.Models
{
    public class CreateNotificationViewModel
    {
        public Guid EventId { get; set; }
        public NotificationTime NotificationTime { get; set; }
    }
}
