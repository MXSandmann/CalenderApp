using ApplicationCore.Models.Entities;

namespace ApplicationCore.Repositories.Contracts
{
    public interface IUserEventRepository
    {
        Task<IEnumerable<UserEvent>> GetAll(string sortBy);
        Task<UserEvent> GetById(Guid id);
        Task<Guid> Add(UserEvent userEvent);
        Task AddRange(IEnumerable<UserEvent> userEvents);
        Task SaveAsync();
        Task Remove(UserEvent userEvent);
        Task Update(UserEvent userEvent);
    }
}
