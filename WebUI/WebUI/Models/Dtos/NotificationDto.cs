using WebUI.Models.Enums;

namespace WebUI.Models.Dtos
{
    public class NotificationDto
    {
        public Guid SubscriptionId { get; set; }        
        public NotificationTime NotificationTime { get; set; }
    }
}
