using ApplicationCore.Models.Enums;

namespace ApplicationCore.Extensions
{
    public static class CertainDaysExtension
    {
        public static bool ShouldOccurOnThisDay(this CertainDays certainDays, DayOfWeek currentDay)
        {
            if (Enum.TryParse<CertainDays>(currentDay.ToString(), out var day))
                return certainDays.HasFlag(day);
            throw new ArgumentOutOfRangeException(nameof(currentDay), $"Could not parse from {currentDay} to enum {nameof(CertainDays)}");
        }
    }
}
