using System.ComponentModel.DataAnnotations;
using WebUI.Models.Dtos;

namespace WebUI.Models
{
    public class CreateSubscriptionViewModel
    {
        public Guid SubscriptionId { get; set; }
        [Display(Name = "Name")]
        public string UserName { get; set; } = string.Empty;
        [Display(Name = "Email")]
        public string UserEmail { get; set; } = string.Empty;
        public List<NotificationDto> Notifications { get; set; } = new();
        public Guid EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
    }    
}
