using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models.Enums
{
    public enum WeekOfTheMonth
    {
        [Display(Name = "No")]
        None,
        [Display(Name = "Every first")]
        First,
        [Display(Name = "Every second")]
        Second,
        [Display(Name = "Every third")]
        Third,
        [Display(Name = "Every fourth")]
        Fourth,
        [Display(Name = "Every fifth")]
        Fifth,
        [Display(Name = "Every last")]
        Last
    }
}
