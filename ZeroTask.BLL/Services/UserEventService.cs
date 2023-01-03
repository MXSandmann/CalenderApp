using ZeroTask.BLL.Services.Contracts;
using ZeroTask.DAL.Repositories.Contracts;
using ZeroTask.DAL.Entities;

namespace ZeroTask.BLL.Services
{
    public class UserEventService : IUserEventService
    {
        private readonly IUserEventRepository _repository;

        public UserEventService(IUserEventRepository repository)
        {
            _repository = repository;
        }

        public async Task AddNewUserEvent(UserEvent userEvent)
        {
            await _repository.Add(userEvent);
        }

        public async Task<UserEvent?> GetUserEventById(int id)
        {
            var userEventFound = await _repository.GetById(id);
            if (userEventFound == null)
                return null;
            return userEventFound;
        }

        public async Task<IEnumerable<UserEvent>> GetUserEvents()
        {
            return await _repository.GetAll();
        }

        public async Task RemoveUserEvent(int id)
        {
            var userEventToDelete = await _repository.GetById(id);
            if (userEventToDelete == null)
                return;
            await _repository.Remove(userEventToDelete);
        }

        public async Task UpdateUserEvent(UserEvent userEvent)
        {

            await _repository.Update(userEvent);
        }                     
    }
}
