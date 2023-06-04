using WebUI.Models.Dtos;

namespace WebUI.Clients.Contracts
{
    public interface IInvitationsClient
    {
        Task<InvitationDto> CreateInvitation(InvitationDto invitationDto);
    }
}
