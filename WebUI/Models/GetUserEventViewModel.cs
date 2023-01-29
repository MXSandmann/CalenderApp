using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RecurrencyEnum = ApplicationCore.Models.Enums.Recurrency;

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

        public static GetUserEventViewModel ToUserEventViewModel(UserEvent userEvent)
        {
            return new GetUserEventViewModel
            {
                Id = userEvent.Id,
                Name = userEvent.Name,
                Category = userEvent.Category,
                Place = userEvent.Place,
                StartTime = userEvent.StartTime,
                EndTime = userEvent.EndTime,
                Date = userEvent.Date,
                LastDate = userEvent.LastDate,
                Description = userEvent.Description,
                AdditionalInfo = userEvent.AdditionalInfo,
                ImageUrl = userEvent.ImageUrl,
                Recurrency = GetRecurrencyDescription(userEvent.RecurrencyRule!)
            };            
        }

        private static string GetRecurrencyDescription(RecurrencyRule recurrencyRule)
        {
            if(recurrencyRule == null)
                return string.Empty;
            var sb = new StringBuilder();
            if (recurrencyRule.OnMonday) sb.Append("On mondays; ");
            if (recurrencyRule.OnTuesday) sb.Append("On tuesdays; ");
            if (recurrencyRule.OnWednesday) sb.Append("On wednesdays; ");
            if (recurrencyRule.OnTuesday) sb.Append("On tuesday; ");
            if (recurrencyRule.OnFriday) sb.Append("On fridays; ");
            if (recurrencyRule.OnSaturday) sb.Append("On saturdays; ");
            if (recurrencyRule.OnSunday) sb.Append("On sundays; ");
            if (recurrencyRule.WeekOfMonth != WeekOfTheMonth.None) sb.Append($"{recurrencyRule.WeekOfMonth.ToString()} week of month; ");
            if (recurrencyRule.MonthOfYear != MonthOfTheYear.None) sb.Append($"{recurrencyRule.MonthOfYear.ToString()} every year; ");
            if (recurrencyRule.Recurrency != RecurrencyEnum.None) sb.Append($"recurrencyRule.Recurrency.ToString(); ");

            return sb.ToString();
        }
    }
}
