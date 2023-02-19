using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Enums
{
    public enum NotificationTime
    {
        None,
        [Display(Name = "In time")]
        InTime,
        [Display(Name = "For 1 minute")]
        For1Min,
        [Display(Name = "For 15 minutes")]
        For15Min,
        [Display(Name = "For 30 minutes")]
        For30in,
        [Display(Name = "For 1 hour")]
        For1Hour,
        [Display(Name = "For 2 hours")]
        For2Hours,
        [Display(Name = "For 1 day")]
        For1Day,
        [Display(Name = "For 1 week")]
        For1Week
    }
}
