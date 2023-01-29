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
            byte value = 0b0000_0000;
            if (onMonday) value &= 0b0000_0001;
            if (onTuesday) value &= 0b0000_0010;
            if (onWednesday) value &= 0b0000_0100;
            if (onThursday) value &= 0b0000_1000;
            if (onFriday) value &= 0b0001_0000;
            if (onSaturday) value &= 0b0010_0000;
            if (onSunday) value &= 0b0100_0000;
            return value;
        }

        public static bool IsOnMonday(byte value)
        {
            var mask = 0b0000_0001;
            return (value & mask) == mask;
        }

        public static bool IsOnTuesday(byte value)
        {
            var mask = 0b0000_0010;
            return (value & mask) == mask;
        }

        public static bool IsOnWednesday(byte value)
        {
            var mask = 0b0000_0100;
            return (value & mask) == mask;
        }

        public static bool IsOnThursday(byte value)
        {
            var mask = 0b0000_1000;
            return (value & mask) == mask;
        }

        public static bool IsOnFriday(byte value)
        {
            var mask = 0b0001_0000;
            return (value & mask) == mask;
        }

        public static bool IsOnSaturday(byte value)
        {
            var mask = 0b0010_0000;
            return (value & mask) == mask;
        }

        public static bool IsOnSunday(byte value)
        {
            var mask = 0b0100_0000;
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
    }
}
