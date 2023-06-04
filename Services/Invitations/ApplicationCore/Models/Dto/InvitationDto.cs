namespace ApplicationCore.Models.Dto
{
    public class InvitationDto
    {
        public Guid EventId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
