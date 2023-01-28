using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models.Enums
{
    public enum MonthOfTheYear
    {
        [Display(Name = "On january")]
        Jan = 1,
        [Display(Name = "On february")]
        Feb,
        [Display(Name = "On march")]
        Mar,
        [Display(Name = "On april")]
        Apr,
        [Display(Name = "On may")]
        May,
        [Display(Name = "On june")]
        Jun,
        [Display(Name = "On july")]
        Jul,
        [Display(Name = "On august")]
        Aug,
        [Display(Name = "On september")]
        Sep,
        [Display(Name = "On oktober")]
        Okt,
        [Display(Name = "On november")]
        Nov,
        [Display(Name = "On december")]
        Dec
    }
}
