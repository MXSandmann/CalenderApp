using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Enums
{
    public enum EvenOdd
    {
        None,
        [Display(Name = "On even days")]
        Even,
        [Display(Name = "On odd days")]
        Odd,
    }
}
