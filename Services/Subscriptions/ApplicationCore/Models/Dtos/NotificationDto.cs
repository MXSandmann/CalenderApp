using ApplicationCore.Models.Enums;

namespace ApplicationCore.Models.Dtos
{
    public class NotificationDto
    {
        public Guid EventId { get; set; }        
        public NotificationTime NotificationTime { get; set; }
    }
}
