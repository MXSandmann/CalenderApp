using ApplicationCore.Models;

namespace ApplicationCore.Repositories.Contracts
{
    public interface IUserEventRepository
    {
        Task<IEnumerable<UserEvent>> GetAll(string sortBy);
        Task<UserEvent> GetById(Guid id);
        Task Add(UserEvent userEvent);
        Task AddRange(IEnumerable<UserEvent> userEvents);
        Task SaveAsync();
        Task Remove(UserEvent userEvent);
        Task Update(UserEvent userEvent);
    }
}
