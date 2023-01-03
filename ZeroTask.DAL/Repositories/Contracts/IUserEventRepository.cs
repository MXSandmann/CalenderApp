using ZeroTask.DAL.Entities;

namespace ZeroTask.DAL.Repositories.Contracts
{
    public interface IUserEventRepository
    {
        Task<IEnumerable<UserEvent>> GetAll();
        Task<UserEvent?> GetById(int id);
        Task Add(UserEvent userEvent);
        Task SaveAsync();
        Task Remove(UserEvent userEvent);
        Task Update(UserEvent userEvent);
    }
}
