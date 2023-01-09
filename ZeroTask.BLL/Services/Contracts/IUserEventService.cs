using ZeroTask.DAL.Entities;

namespace ZeroTask.BLL.Services.Contracts
{
    public interface IUserEventService
    {
        Task AddNewUserEvent(UserEvent userEvent);
        Task RemoveUserEvent(Guid id);
        Task UpdateUserEvent(UserEvent userEvent);
        Task<IEnumerable<UserEvent>> GetUserEvents();
        Task<UserEvent?> GetUserEventById(Guid id);
    }
}
