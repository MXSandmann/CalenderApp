using WebUI.Models.Enums;

namespace WebUI.Models.Dtos
{
    public class NotificationDto
    {
        public Guid EventId { get; set; }        
        public NotificationTime NotificationTime { get; set; }
    }
}
