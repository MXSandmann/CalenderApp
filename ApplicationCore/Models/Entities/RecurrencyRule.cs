using ApplicationCore.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models.Entities
{
    public class RecurrencyRule
    {
        [Key]
        public Guid Id { get; set; }
        public UserEvent UserEvent { get; set; } = null!;
        public Guid UserEventId { get; set; }
        /// <summary>
        /// Classic recurrency - daily, weekly, monthly and yearly
        /// </summary>
        public Recurrency Recurrency { get; set; }
        /// <summary>
        /// How big is the gap between events
        /// Ex: Recerrency type: daily, Gap = 1
        /// => The gap is 1 day
        /// </summary>
        public int Gap { get; set; }
        public int MaximumOccurrencies { get; set; }
        public DayOfTheWeek DayOfWeek { get; set; }
        public WeekOfTheMonth WeekOfMonth { get; set; }
        public MonthOfTheYear MonthOfYear { get; set; }
    }
}
