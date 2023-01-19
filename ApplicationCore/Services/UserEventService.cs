using ApplicationCore.Models;
using ApplicationCore.Models.Enums;
using ApplicationCore.Repositories.Contracts;
using ApplicationCore.Services.Contracts;

namespace ApplicationCore.Services
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
            switch(userEvent.Recurrency)
            {
                case Recurrency.None:
                    {
                        await _repository.Add(userEvent);
                        break;
                    }


                case Recurrency.Daily:
                    {
                        // While start day + iterator < end day => create 
                        var events = new List<UserEvent>
                        {
                            userEvent
                        };
                        var dateOfNextEvent = userEvent.StartDateTime;
                        while(dateOfNextEvent < userEvent.EndDateTime)
                        {
                            dateOfNextEvent = dateOfNextEvent.AddDays(1);
                            var nextUserEvent = UserEvent.Copy(userEvent);
                            nextUserEvent.StartDateTime = dateOfNextEvent;                                                        
                            events.Add(nextUserEvent);
                        }
                        await _repository.AddRange(events);
                        break;
                    }
                case Recurrency.Weekly:
                    {

                        break;
                    }
                case Recurrency.Monthly:
                    {

                        break;
                    }
                case Recurrency.Yearly:
                    {

                        break;
                    }

            }
            return userEvent;
            
        }

        public async Task<UserEvent> GetUserEventById(Guid id)
        {
            var userEventFound = await _repository.GetById(id);            
            ArgumentNullException.ThrowIfNull(userEventFound);
            return userEventFound;
        }

        public async Task<IEnumerable<UserEvent>> GetUserEvents(string sortBy)
        {
            return await _repository.GetAll(sortBy);
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
