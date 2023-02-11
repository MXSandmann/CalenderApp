using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebUI.Extensions;
using WebUI.Models.Dtos;
using WebUI.Models.Enums;
using RecurrencyEnum = WebUI.Models.Enums.Recurrency;

namespace WebUI.Models
{
    public class GetUserEventViewModel
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
        public string Recurrency { get; set; } = null!;

        public static string GetRecurrencyDescription(RecurrencyRuleDto recurrencyRule)
        {
            if (recurrencyRule == null)
                return string.Empty;
            var sb = new StringBuilder();

            if (recurrencyRule.DayOfWeek.HasFlag(CertainDays.Monday)) sb.Append("On mondays; ");
            if (recurrencyRule.DayOfWeek.HasFlag(CertainDays.Tuesday)) sb.Append("On tuesdays; ");
            if (recurrencyRule.DayOfWeek.HasFlag(CertainDays.Wednesday)) sb.Append("On wednesdays; ");
            if (recurrencyRule.DayOfWeek.HasFlag(CertainDays.Thursday)) sb.Append("On thursdays; ");
            if (recurrencyRule.DayOfWeek.HasFlag(CertainDays.Friday)) sb.Append("On fridays; ");
            if (recurrencyRule.DayOfWeek.HasFlag(CertainDays.Saturday)) sb.Append("On saturdays; ");
            if (recurrencyRule.DayOfWeek.HasFlag(CertainDays.Sunday)) sb.Append("On sundays; ");
            if (recurrencyRule.WeekOfMonth != WeekOfTheMonth.None) sb.Append($"{recurrencyRule.WeekOfMonth.ToString()} week of month; ");
            if (recurrencyRule.Recurrency != RecurrencyEnum.None) sb.Append(recurrencyRule.Recurrency.ToString() + "; ");
            if (recurrencyRule.EvenOdd != EvenOdd.None) sb.Append(recurrencyRule.EvenOdd.GetDisplayName() + "; ");

            return sb.ToString();
        }
    }
}
