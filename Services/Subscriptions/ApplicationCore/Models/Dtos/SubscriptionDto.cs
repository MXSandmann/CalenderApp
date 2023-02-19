namespace ApplicationCore.Models.Dtos
{
    public class SubscriptionDto
    {
        public Guid SubscriptionId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public Guid EventId { get; set; }
        public IEnumerable<NotificationDto>? Notifications { get; set; }
    }
}
