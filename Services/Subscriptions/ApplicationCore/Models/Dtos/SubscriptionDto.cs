namespace ApplicationCore.Models.Dtos
{
    public class SubscriptionDto
    {
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public Guid EventId { get; set; }
    }
}
