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
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Description { get; set; } = null!;
        public string AdditionalInfo { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public Recurrency Recurrency { get; set; }
    }
}
