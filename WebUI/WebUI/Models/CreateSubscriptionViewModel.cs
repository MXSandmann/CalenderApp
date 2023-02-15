using System.ComponentModel.DataAnnotations;
using WebUI.Models.Enums;

namespace WebUI.Models
{
    public class CreateSubscriptionViewModel
    {
        [Display(Name = "Name")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string UserEmail { get; set; }
        [Display(Name = "Send notifications")]
        public bool HasNotification { get; set; }
        [Display(Name = "Notify before event")]
        public NotificationTime NotificationTime { get; set; }
    }    
}
