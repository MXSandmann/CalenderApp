using ApplicationCore.Models;

namespace ApplicationCore.Services.Contracts
{
    public interface IUserEventService
    {
        Task<UserEvent> AddNewUserEvent(UserEvent userEvent);
        Task RemoveUserEvent(Guid id);
        Task<UserEvent> UpdateUserEvent(UserEvent userEvent);
        Task<IEnumerable<UserEvent>> GetUserEvents();
        Task<UserEvent> GetUserEventById(Guid id);
    }
}
