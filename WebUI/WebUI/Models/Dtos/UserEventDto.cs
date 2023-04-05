using System.ComponentModel.DataAnnotations;
using WebUI.Models.Enums;

namespace WebUI.Models.Dtos
{
    public class UserEventDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Place { get; set; } = null!;
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastDate { get; set; }
        public string Description { get; set; } = null!;
        public string AdditionalInfo { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public bool HasRecurrency { get; set; }
        public RecurrencyRuleDto? RecurrencyRule { get; set; }
        public Guid? InstructorId { get; set; }
        public bool Done { get; set; }
    }

    public class RecurrencyRuleDto
    {
        public Guid Id { get; set; }
        public Guid UserEventId { get; set; }
        public Recurrency Recurrency { get; set; }
        public CertainDays DayOfWeek { get; set; }
        public WeekOfTheMonth WeekOfMonth { get; set; }
        public EvenOdd EvenOdd { get; set; }
    }
}
