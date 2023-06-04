using WebUI.Models.Enums;

namespace WebUI.Models.ViewModels
{
    public class CreateInvitationViewModel
    {
        public Guid EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public RoleToInvite Role { get; set; }
    }
}
