using ZeroTask.BLL.Models;

namespace ZeroTask.BLL.Repositories.Contracts
{
    public interface IUserEventRepository
    {
        Task<IEnumerable<UserEvent>> GetAll();
        Task<UserEvent> GetById(Guid id);
        Task Add(UserEvent userEvent);
        Task SaveAsync();
        Task Remove(UserEvent userEvent);
        Task Update(UserEvent userEvent);
    }
}
