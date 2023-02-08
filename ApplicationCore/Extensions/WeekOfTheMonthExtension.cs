using ApplicationCore.Models.Enums;

namespace ApplicationCore.Extensions
{
    public static class WeekOfTheMonthExtension
    {
        public static bool IsOnGivenWeekOfMonth(this WeekOfTheMonth weekOfTheMonth, DateTime day)
        {
            // If not the last week
            if(weekOfTheMonth != WeekOfTheMonth.Last)
            {
                // Get a week of the month by dividing
                var week = (day.Day / 7) + 1;
                // Compare calculated int with int value of the WeekOfTheMonth enum
                return (int)weekOfTheMonth == week;
            }
            // The day on the last week of month
            return (31 - day.Day) / 7 == 0;
            
        }
    }
}
