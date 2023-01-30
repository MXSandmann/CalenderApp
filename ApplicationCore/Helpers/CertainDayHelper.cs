using ApplicationCore.Constants;
using ApplicationCore.Models.Enums;

namespace ApplicationCore.Helpers
{
    public static class CertainDayHelper
    {
        public static byte GetByteValue(bool onMonday,
            bool onTuesday,
            bool onWednesday,
            bool onThursday,
            bool onFriday,
            bool onSaturday,
            bool onSunday)
        {
            byte value = DaysAsBinary.EMPTY;
            if (onMonday) value |= DaysAsBinary.MONDAY;
            if (onTuesday) value |= DaysAsBinary.TUESDAY;
            if (onWednesday) value |= DaysAsBinary.WEDNESDAY;
            if (onThursday) value |= DaysAsBinary.THURSDAY;
            if (onFriday) value |= DaysAsBinary.FRIDAY;
            if (onSaturday) value |= DaysAsBinary.SATURDAY;
            if (onSunday) value |= DaysAsBinary.SUNDAY;
            return value;
        }

        public static bool IsOnMonday(byte value)
        {
            var mask = DaysAsBinary.MONDAY;
            return (value & mask) == mask;
        }

        public static bool IsOnTuesday(byte value)
        {
            var mask = DaysAsBinary.TUESDAY;
            return (value & mask) == mask;
        }

        public static bool IsOnWednesday(byte value)
        {
            var mask = DaysAsBinary.WEDNESDAY;
            return (value & mask) == mask;
        }

        public static bool IsOnThursday(byte value)
        {
            var mask = DaysAsBinary.THURSDAY;
            return (value & mask) == mask;
        }

        public static bool IsOnFriday(byte value)
        {
            var mask = DaysAsBinary.FRIDAY;
            return (value & mask) == mask;
        }

        public static bool IsOnSaturday(byte value)
        {
            var mask = DaysAsBinary.SATURDAY;
            return (value & mask) == mask;
        }

        public static bool IsOnSunday(byte value)
        {
            var mask = DaysAsBinary.SUNDAY;
            return (value & mask) == mask;
        }

        public static bool IsOnWeekend(byte value)
        {
            var mask = DaysAsBinary.WEEKEND;
            return (value & mask) == mask;
        }

        public static bool IsOnWorkingDays(byte value)
        {
            var mask = DaysAsBinary.WORKINGDAYS;
            return (value & mask) == mask;
        }

        public static bool ShouldOccurOnThisDay(DayOfWeek day, byte days)
        {
            return day switch
            {
                DayOfWeek.Monday => IsOnMonday(days),
                DayOfWeek.Tuesday => IsOnTuesday(days),
                DayOfWeek.Wednesday => IsOnWednesday(days),
                DayOfWeek.Thursday => IsOnThursday(days),
                DayOfWeek.Friday => IsOnFriday(days),
                DayOfWeek.Saturday => IsOnSaturday(days),
                DayOfWeek.Sunday => IsOnSunday(days),
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
