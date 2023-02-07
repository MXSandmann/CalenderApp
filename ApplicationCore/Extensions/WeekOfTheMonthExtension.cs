using ApplicationCore.Models.Enums;

namespace ApplicationCore.Extensions
{
    public static class WeekOfTheMonthExtension
    {
        public static bool IsOnGivenWeekOfMonth(this WeekOfTheMonth weekOfTheMonth, DateTime day)
        {
            // Get a week of the month by dividing
            var week = (day.Day / 7) + 1;
            // Compare calculated int with int value of the WeekOfTheMonth enum
            return (int)weekOfTheMonth == week;
        }
    }
}
