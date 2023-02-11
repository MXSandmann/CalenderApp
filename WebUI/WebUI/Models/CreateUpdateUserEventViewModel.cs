using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebUI.Models.Enums;

namespace WebUI.Models
{

    public class CreateUpdateUserEventViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Place { get; set; } = null!;
        [BindProperty, DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [BindProperty, DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime? LastDate { get; set; }
        public string Description { get; set; } = null!;
        public string AdditionalInfo { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public bool HasRecurrency { get; set; }
        public Recurrency? Recurrency { get; set; }
        public bool OnMonday { get; set; }
        public bool OnTuesday { get; set; }
        public bool OnWednesday { get; set; }
        public bool OnThursday { get; set; }
        public bool OnFriday { get; set; }
        public bool OnSaturday { get; set; }
        public bool OnSunday { get; set; }
        public WeekOfTheMonth? WeekOfMonth { get; set; }
        public bool OnWorkingDays { get; set; }
        public bool OnWeekend { get; set; }
        public EvenOdd EvenOdd { get; set; }

        public static bool IsOnWeekend(CertainDays certainDays)
        {
            return certainDays.HasFlag(CertainDays.Saturday)
                && certainDays.HasFlag(CertainDays.Sunday);
        }

        public static bool IsOnWorkingDays(CertainDays certainDays)
        {
            return certainDays.HasFlag(CertainDays.Monday)
                && certainDays.HasFlag(CertainDays.Tuesday)
                && certainDays.HasFlag(CertainDays.Wednesday)
                && certainDays.HasFlag(CertainDays.Thursday)
                && certainDays.HasFlag(CertainDays.Friday);
        }
    }    
}
