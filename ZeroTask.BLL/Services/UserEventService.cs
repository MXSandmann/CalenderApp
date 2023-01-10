using ZeroTask.BLL.Services.Contracts;
using ZeroTask.DAL.Entities;
using ZeroTask.BLL.Repositories.Contracts;

namespace ZeroTask.BLL.Services
{
    public class UserEventService : IUserEventService
    {
        private readonly IUserEventRepository _repository;

        public UserEventService(IUserEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserEvent> AddNewUserEvent(UserEvent userEvent)
        {
            await _repository.Add(userEvent);
            return userEvent;
        }

        public async Task<UserEvent> GetUserEventById(Guid id)
        {
            var userEventFound = await _repository.GetById(id);            
            ArgumentNullException.ThrowIfNull(userEventFound);
            return userEventFound;
        }

        public async Task<IEnumerable<UserEvent>> GetUserEvents()
        {
            return await _repository.GetAll();
        }

        public async Task RemoveUserEvent(Guid id)
        {
            var userEventToDelete = await _repository.GetById(id);
            ArgumentNullException.ThrowIfNull(userEventToDelete);
            await _repository.Remove(userEventToDelete);
        }

        public async Task<UserEvent> UpdateUserEvent(UserEvent userEvent)
        {
            await _repository.Update(userEvent);
            return userEvent;
        }                     
    }
}
