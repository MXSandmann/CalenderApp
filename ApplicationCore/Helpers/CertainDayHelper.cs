using ApplicationCore.Models.Enums;

namespace ApplicationCore.Helpers
{
    public static class CertainDayHelper
    {
        public static CertainDays GetCertainsDaysValue(bool onMonday,
            bool onTuesday,
            bool onWednesday,
            bool onThursday,
            bool onFriday,
            bool onSaturday,
            bool onSunday)
        {
            var result = onMonday ? CertainDays.Monday : 0;
            result |= onTuesday ? CertainDays.Tuesday : 0;
            result |= onWednesday ? CertainDays.Wednesday : 0;
            result |= onThursday ? CertainDays.Thursday : 0;
            result |= onFriday ? CertainDays.Friday : 0;
            result |= onSaturday ? CertainDays.Saturday : 0;
            result |= onSunday ? CertainDays.Sunday : 0;
            return result;
        }

        public static bool ShouldOccurOnThisDay(DayOfWeek day, CertainDays days)
        {
            return day switch
            {
                DayOfWeek.Monday => days.HasFlag(CertainDays.Monday),
                DayOfWeek.Tuesday => days.HasFlag(CertainDays.Tuesday),
                DayOfWeek.Wednesday => days.HasFlag(CertainDays.Wednesday),
                DayOfWeek.Thursday => days.HasFlag(CertainDays.Thursday),
                DayOfWeek.Friday => days.HasFlag(CertainDays.Friday),
                DayOfWeek.Saturday => days.HasFlag(CertainDays.Saturday),
                DayOfWeek.Sunday => days.HasFlag(CertainDays.Sunday),
                _ => throw new ArgumentOutOfRangeException(nameof(day)),
            };
        }

        public static bool IsOnGivenWeekOfMonth(WeekOfTheMonth order, DateTime day)
        {
            return order switch
            {
                WeekOfTheMonth.First => (day.Day >= 1 && day.Day <= 7),
                WeekOfTheMonth.Second => (day.Day >= 8 && day.Day <= 14),
                WeekOfTheMonth.Third => (day.Day >= 15 && day.Day <= 21),
                WeekOfTheMonth.Fourth => (day.Day >= 22 && day.Day <= 28),
                WeekOfTheMonth.Fifth => (day.Day >= 29 && day.Day <= 31),
                _ => throw new ArgumentOutOfRangeException(nameof(order)),
            };
        }
    }
}
