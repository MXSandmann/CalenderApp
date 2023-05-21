using ApplicationCore.Models.Entities;

namespace ApplicationCore.Services.Contracts
{
    public interface IInvitationService
    {
        Task<Invitation> AddInvitation(Invitation invitation);
        Task<IEnumerable<Invitation>> GetAllInvitations();
    }
}
