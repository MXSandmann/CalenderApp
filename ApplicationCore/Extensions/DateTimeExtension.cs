using ApplicationCore.Models.Enums;

namespace ApplicationCore.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime AddTimePeriod(this DateTime dateTime, Recurrency recurrency)
        {
            // Initialize adding period functionality
            var dict = new Dictionary<Recurrency, Func<DateTime, DateTime>>
                {
                    { Recurrency.Daily, (date) => date.AddDays(1) },
                    { Recurrency.Weekly, (date) => date.AddDays(7) },
                    { Recurrency.Monthly, (date) => date.AddMonths(1) },
                    { Recurrency.Yearly, (date) => date.AddYears(1) },
                    { Recurrency.None, (date) => date }
                };

            if (!dict.TryGetValue(recurrency, out var AddPeriod))
                throw new ArgumentException($"The value of {nameof(recurrency)} unknown");

            return AddPeriod(dateTime);
        }
    }
}
