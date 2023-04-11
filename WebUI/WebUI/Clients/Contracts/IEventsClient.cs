using WebUI.Models;
using WebUI.Models.Dtos;

namespace WebUI.Clients.Contracts
{
    public interface IEventsClient
    {
        Task<IEnumerable<UserEventDto>> GetUserEvents(string sortBy, Guid userId);
        Task<IEnumerable<UserEventDto>> GetUserEvents();
        Task<UserEventDto> AddNewUserEvent(UserEventDto userEventDto, RecurrencyRuleDto recurrencyRuleDto);
        Task<IEnumerable<CalendarEvent>> GetCalendarEvents(Guid userId);
        Task<UserEventDto> GetUserEventById(Guid id);
        Task<UserEventDto> UpdateUserEvent(UserEventDto userEventDto, RecurrencyRuleDto recurrencyRuleDto);
        Task RemoveUserEvent(Guid id);
        Task AddUserEventNamesForSubscriptions(IEnumerable<SubscriptionDto> subscriptions);
        Task<(IEnumerable<UserEventDto>, int)> GetSearchResults(SearchUserEventsDto searchDto);
        Task<IEnumerable<UserActivityRecordDto>> GetAllActivities();
        Task<FileDto?> DownloadEventAsFile(Guid eventId);
        Task<FileDto?> MultipleDownloadEventAsFile(IEnumerable<Guid> eventIds);
        Task<UserEventDto> AssignInstructorToEvent(Guid eventId, Guid instructorId);
        Task<UserEventDto> MarkAsDone(Guid eventId);
    }
}
