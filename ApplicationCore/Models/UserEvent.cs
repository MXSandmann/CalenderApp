using ApplicationCore.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models
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
        public Recurrency Recurrency { get; set; }

        public static UserEvent Copy(UserEvent other)
        {
            return new UserEvent
            {
                Id = other.Id,
                Name = other.Name,
                Category = other.Category,
                Place = other.Place,
                StartTime = other.StartTime,
                EndTime = other.EndTime,
                Date = other.Date,
                LastDate = other.LastDate,
                Description = other.Description,
                AdditionalInfo = other.AdditionalInfo,
                ImageUrl = other.ImageUrl,
                Recurrency = other.Recurrency
            };

        }
    }
}
