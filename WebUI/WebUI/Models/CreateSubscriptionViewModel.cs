using System.ComponentModel.DataAnnotations;
using System.Text;
using WebUI.Models.Dtos;
using WebUI.Models.Enums;

namespace WebUI.Models
{
    public class CreateSubscriptionViewModel
    {
        [Display(Name = "Name")]
        public string UserName { get; set; } = string.Empty;
        [Display(Name = "Email")]
        public string UserEmail { get; set; } = string.Empty;
        [Display(Name = "Send notifications")]
        public bool HasNotification { get; set; }

        public List<NotificationDto> Notifications { get; set; } = new();

        public Guid EventId { get; set; }
        public string EventName { get; set; } = string.Empty;

        private string NotificationsToString(List<NotificationDto> notifications)
        {
            if (!notifications.Any()) return string.Empty;
            var sb = new StringBuilder();
            foreach (var item in notifications)
            {
                sb.Append(item.ToString()).Append("; ");
            }
            return sb.ToString();
        }
    }    

    
}
