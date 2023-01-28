using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models.Enums
{
    public enum WeekOfTheMonth
    {
        [Display(Name = "Every first")]
        First = 1,
        [Display(Name = "Every second")]
        Second,
        [Display(Name = "Every third")]
        Third,
        [Display(Name = "Every fourth")]
        Fourth
    }
}
