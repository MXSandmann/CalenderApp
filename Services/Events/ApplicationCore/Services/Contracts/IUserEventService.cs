using ApplicationCore.Models;
using ApplicationCore.Models.Dtos;
using ApplicationCore.Models.Entities;

namespace ApplicationCore.Services.Contracts
{
    public interface IUserEventService
    {
        Task<UserEvent> AddNewUserEvent(UserEvent userEvent, RecurrencyRule recurrencyRule);
        Task RemoveUserEvent(Guid id);
        Task<UserEvent> UpdateUserEvent(UserEvent userEvent, RecurrencyRule recurrencyRule);
        Task<IEnumerable<UserEvent>> GetUserEvents(Guid userId);
        Task<UserEvent> GetUserEventById(Guid id);
        Task<IEnumerable<CalendarEvent>> GetCalendarEvents(Guid userId);
        Task<Dictionary<Guid, string>> GetEventNames(IEnumerable<Guid> eventIds);
        Task<PaginationResponse<UserEventDto>> SearchUserEvents(string entry, int limit, int offset);
        Task<string> DownloadICSFile(Guid eventId);
        Task<UserEvent> AssignInstructorToEvent(Guid eventId, Guid instructorId);
        Task<UserEvent> MarkAsDone(Guid id);
    }
}
