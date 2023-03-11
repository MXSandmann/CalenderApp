using System.Text;
using WebUI.Models.Dtos;

namespace WebUI.Models
{
    public class GetSubscriptionViewModel
    {        
        public string UserName { get; set; } = string.Empty;
     
        public string UserEmail { get; set; } = string.Empty;

        public Guid EventId { get; set; }
        public Guid SubscriptionId { get; set; }

        public string Notifications { get; set; } = string.Empty;
                
        public string EventName { get; set; } = string.Empty;

        public static string NotificationsToString(IEnumerable<NotificationDto> notifications)
        {
            if (notifications == null || !notifications.Any()) return string.Empty;
            var sb = new StringBuilder();
            foreach (var item in notifications)
            {
                sb.Append(item.NotificationTime.ToString()).Append("; ");
            }
            return sb.ToString();
        }
    }
}
