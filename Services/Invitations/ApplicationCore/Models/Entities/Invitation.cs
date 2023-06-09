using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models.Entities
{
    public class Invitation
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? EventId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}