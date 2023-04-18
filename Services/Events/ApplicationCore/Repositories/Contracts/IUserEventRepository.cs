using ApplicationCore.Models.Entities;

namespace ApplicationCore.Repositories.Contracts
{
    public interface IUserEventRepository
    {
        Task<IEnumerable<UserEvent>> GetAll();
        Task<IEnumerable<UserEvent>> GetAll(Guid userId);
        Task<UserEvent> GetById(Guid id);
        Task<IEnumerable<UserEvent>> GetManyById(IEnumerable<Guid> ids);
        Task<Guid> Add(UserEvent userEvent);
        Task AddRange(IEnumerable<UserEvent> userEvents);
        Task SaveAsync();
        Task Remove(UserEvent userEvent);
        Task<UserEvent> Update(UserEvent userEvent);
        Task<Dictionary<Guid, string>> GetEventNames(IEnumerable<Guid> eventIds);
        Task<(IEnumerable<UserEvent>, int)> SearchUserEvents(string entry, int limit, int offset);
        Task<UserEvent> AssignInstructor(Guid eventId, Guid instructorId);
        Task<UserEvent> MarkAsDone(Guid id);
    }
}
