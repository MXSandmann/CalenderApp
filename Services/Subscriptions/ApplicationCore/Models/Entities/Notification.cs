using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ApplicationCore.Models.Enums;

namespace ApplicationCore.Models.Entities
{
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SubscriptionId { get; set; }
        [NotNull]
        public Subscription Subscription { get; set; } = null!;
        public NotificationTime NotificationTime { get; set; }
    }
}
