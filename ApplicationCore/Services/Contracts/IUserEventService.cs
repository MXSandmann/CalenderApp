using ApplicationCore.Models.Entities;

namespace ApplicationCore.Services.Contracts
{
    public interface IUserEventService
    {
        Task<UserEvent> AddNewUserEvent(UserEvent userEvent, RecurrencyRule recurrencyRule);
        Task RemoveUserEvent(Guid id);
        Task<UserEvent> UpdateUserEvent(UserEvent userEvent);
        Task<IEnumerable<UserEvent>> GetUserEvents(string sortBy);
        Task<UserEvent> GetUserEventById(Guid id);
    }
}
