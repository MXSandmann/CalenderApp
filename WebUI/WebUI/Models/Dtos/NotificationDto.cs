using WebUI.Models.Enums;

namespace WebUI.Models.Dtos
{
    public class NotificationDto
    {
        public Guid SubscriptionId { get; set; }        
        public DateTime NotificationTime { get; set; }
        public string EventName { get; set; } = string.Empty;
    }
}
