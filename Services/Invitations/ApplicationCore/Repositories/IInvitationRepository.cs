using ApplicationCore.Models.Entities;

namespace ApplicationCore.Repositories
{
    public interface IInvitationRepository
    {
        Task<Guid> Add(Invitation invitation);
        Task<Invitation> GetById(Guid id);
        Task<IEnumerable<Invitation>> GetAll();
    }
}
