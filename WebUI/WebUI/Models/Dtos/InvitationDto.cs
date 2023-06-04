namespace WebUI.Models.Dtos
{
    public class InvitationDto
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string EventName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
