using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using ApplicationCore.Repositories.Contracts;
using ApplicationCore.Services.Contracts;

namespace ApplicationCore.Services
{
    public class UserEventService : IUserEventService
    {
        private readonly IUserEventRepository _userEventRepository;
        private readonly IRecurrencyRuleRepository _recurrencyRuleRepository;

        public UserEventService(IUserEventRepository repository, IRecurrencyRuleRepository recurrencyRuleRepository)
        {
            _userEventRepository = repository;
            _recurrencyRuleRepository = recurrencyRuleRepository;
        }
        public async Task<UserEvent> AddNewUserEvent(UserEvent userEvent, RecurrencyRule recurrencyRule)
        {
            // First add a new event to the db
            var newUserEventId = await _userEventRepository.Add(userEvent);

            // Next check if there is an incoming recurrency for this event
            if(userEvent.HasRecurrency == YesNo.Yes)
            {
                ArgumentNullException.ThrowIfNull(recurrencyRule);
                recurrencyRule.UserEventId = newUserEventId;
                await _recurrencyRuleRepository.Add(recurrencyRule);                
            }
            var newUserEvent = await _userEventRepository.GetById(newUserEventId);
            ArgumentNullException.ThrowIfNull(newUserEvent);
            return newUserEvent;
        }
        //public async Task<IEnumerable<UserEvent>> AddNewUserEvent(UserEvent userEvent, RecurrencyRule recurrencyRule)
        //{
        //    var results = new List<UserEvent>();
            //switch(userEvent.RecurrencyRule.Recurrency)
            //{
            //    case Recurrency.None:
            //        {
            //            await _repository.Add(userEvent);
            //            results.Add(userEvent);
            //            break;
            //        }

            //    case Recurrency.Daily:
            //        {
            //            var events = CompleteEventsForPeriod(userEvent, Recurrency.Daily);                        
            //            await _repository.AddRange(events);
            //            results.AddRange(events);
            //            break;
            //        }
            //    case Recurrency.Weekly:
            //        {
            //            var events = CompleteEventsForPeriod(userEvent, Recurrency.Weekly);
            //            await _repository.AddRange(events);
            //            results.AddRange(events);
            //            break;
            //        }
            //    case Recurrency.Monthly:
            //        {
            //            var events = CompleteEventsForPeriod(userEvent, Recurrency.Monthly);
            //            await _repository.AddRange(events);
            //            results.AddRange(events);
            //            break;
            //        }
            //    case Recurrency.Yearly:
            //        {
            //            var events = CompleteEventsForPeriod(userEvent, Recurrency.Yearly);
            //            await _repository.AddRange(events);
            //            results.AddRange(events);
            //            break;
            //        }
            //    default:
            //        {
            //            throw new ArgumentException($"The value of {nameof(userEvent.RecurrencyRule.Recurrency)} unknown");
            //        }
            //}
        //    return results;            
        //}

        private static List<UserEvent> CompleteEventsForPeriod(UserEvent userEvent, Recurrency recurrency)
        {
            var events = new List<UserEvent>
            {
                userEvent
            };
            var dateOfNextEvent = userEvent.Date;
            while (dateOfNextEvent < userEvent.LastDate)
            {

                dateOfNextEvent = AddTimePeriod(dateOfNextEvent, recurrency);
                var nextUserEvent = UserEvent.Copy(userEvent);
                nextUserEvent.Date = dateOfNextEvent;
                events.Add(nextUserEvent);
            }
            return events;
        }

        private static DateTime AddTimePeriod(DateTime dateOfNextEvent, Recurrency recurrency)
        {
            return recurrency switch
            {
                Recurrency.Daily => dateOfNextEvent.AddDays(1),
                Recurrency.Weekly => dateOfNextEvent.AddDays(7),
                Recurrency.Monthly => dateOfNextEvent.AddMonths(1),
                Recurrency.Yearly => dateOfNextEvent.AddYears(1),
                Recurrency.None => dateOfNextEvent,
                _ => throw new ArgumentException($"The value of {nameof(recurrency)} unknown")
            };
        }

        public async Task<UserEvent> GetUserEventById(Guid id)
        {
            var userEventFound = await _userEventRepository.GetById(id);
            ArgumentNullException.ThrowIfNull(userEventFound);
            return userEventFound;
        }        

        public async Task<IEnumerable<UserEvent>> GetUserEvents(string sortBy)
        {
            return await _userEventRepository.GetAll(sortBy);
        }

        public async Task RemoveUserEvent(Guid id)
        {
            var userEventToDelete = await _userEventRepository.GetById(id);
            ArgumentNullException.ThrowIfNull(userEventToDelete);
            await _userEventRepository.Remove(userEventToDelete);
        }

        public async Task<UserEvent> UpdateUserEvent(UserEvent userEvent)
        {
            await _userEventRepository.Update(userEvent);
            return userEvent;
        }                     
    }
}
