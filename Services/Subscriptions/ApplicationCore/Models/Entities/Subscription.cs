using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models.Entities
{
    public class Subscription
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public Guid EventId { get; set; }
    }
}
