using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models.Entities
{
    public class UserEvent
    {
        [Key]
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
        public RecurrencyRule? RecurrencyRule { get; set; }
        public Guid? InstructorId { get; set; }
    }
}
