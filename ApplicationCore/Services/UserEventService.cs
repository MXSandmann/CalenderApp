using ApplicationCore.Helpers;
using ApplicationCore.Models;
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
            if (userEvent.HasRecurrency)
            {
                ArgumentNullException.ThrowIfNull(recurrencyRule);
                recurrencyRule.UserEventId = newUserEventId;
                await _recurrencyRuleRepository.Add(recurrencyRule);
            }
            var newUserEvent = await _userEventRepository.GetById(newUserEventId);
            ArgumentNullException.ThrowIfNull(newUserEvent);
            return newUserEvent;
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

        public async Task<UserEvent> UpdateUserEvent(UserEvent userEvent, RecurrencyRule recurrencyRule)
        {
            var userEventUpdated = await _userEventRepository.Update(userEvent);

            // If user event doesn't have any recurrency, simply return
            if (!userEventUpdated.HasRecurrency)
                return userEvent;

            // If user event no recurrency before, but now should have one, create a recurrency
            if (userEventUpdated.RecurrencyRule == null)
            {
                recurrencyRule.UserEventId = userEventUpdated.Id;
                await _recurrencyRuleRepository.Add(recurrencyRule);
                return userEvent;
            }

            // If user event had a recurrency before, and should remain it, get id of this recurrency rule            
            recurrencyRule.Id = userEventUpdated.RecurrencyRule.Id;
            await _recurrencyRuleRepository.Update(recurrencyRule);
            return userEvent;
        }

        public async Task<IEnumerable<CalendarEvent>> GetCalendarEvents()
        {
            var userEvents = await _userEventRepository.GetAll(string.Empty);

            if (!userEvents.Any()) return Enumerable.Empty<CalendarEvent>();

            var calendarEvents = new List<CalendarEvent>();

            foreach (var userEvent in userEvents)
            {
                var rule = userEvent.RecurrencyRule;

                // 1. Check if has recurrency
                if (rule == null)
                {
                    calendarEvents.Add(CalendarEvent.ToCalendarEvent(userEvent));
                    continue;
                }

                // 2. Check standard recurrency
                if (rule.Recurrency != Recurrency.None)
                    calendarEvents.AddRange(CreateCalendarEventsWithStandardRecurrency(userEvent, rule.Recurrency));


                // 3. Check if certain day of week is set
                if (rule.DayOfWeek != default)
                    calendarEvents.AddRange(CreateCalendarEventsWithCertainDays(userEvent, rule.DayOfWeek, rule.WeekOfMonth));

                // 4. Check if even or odd day
                if (rule.EvenOdd != default)
                    calendarEvents.AddRange(CreateCalendarEventsWithEvenOrOddDays(userEvent, rule.EvenOdd));

            }
            return calendarEvents;
        }

        private static IEnumerable<CalendarEvent> CreateCalendarEventsWithStandardRecurrency(UserEvent userEvent, Recurrency recurrency)
        {
            var calendarEvents = new List<CalendarEvent>();
            switch (recurrency)
            {
                case Recurrency.Daily:
                    {
                        var events = CompleteEventsForPeriod(userEvent, Recurrency.Daily);

                        calendarEvents.AddRange(events);
                        break;
                    }
                case Recurrency.Weekly:
                    {
                        var events = CompleteEventsForPeriod(userEvent, Recurrency.Weekly);

                        calendarEvents.AddRange(events);
                        break;
                    }
                case Recurrency.Monthly:
                    {
                        var events = CompleteEventsForPeriod(userEvent, Recurrency.Monthly);
                        calendarEvents.AddRange(events);
                        break;
                    }
                case Recurrency.Yearly:
                    {
                        var events = CompleteEventsForPeriod(userEvent, Recurrency.Yearly);
                        calendarEvents.AddRange(events);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException($"The value of {nameof(recurrency)} unknown");
                    }
            }
            return calendarEvents;
        }

        private static List<CalendarEvent> CompleteEventsForPeriod(UserEvent userEvent, Recurrency recurrency)
        {
            var firstCalendarEvent = CalendarEvent.ToCalendarEvent(userEvent);
            var events = new List<CalendarEvent>
            {
                firstCalendarEvent
            };
            var dateOfNextEvent = userEvent.Date;
            while (dateOfNextEvent < userEvent.LastDate)
            {

                dateOfNextEvent = AddTimePeriod(dateOfNextEvent, recurrency);
                var nextCalendarEvent = CreateCalendarEvent(firstCalendarEvent, dateOfNextEvent);
                events.Add(nextCalendarEvent);
            }
            return events;
        }

        private static CalendarEvent CreateCalendarEvent(CalendarEvent firstCalendarEvent, DateTime dateOfNextEvent)
        {
            var nextCalendarEvent = (CalendarEvent)firstCalendarEvent.Clone();
            nextCalendarEvent.Start = dateOfNextEvent;
            nextCalendarEvent.End = dateOfNextEvent;
            return nextCalendarEvent;
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

        private static IEnumerable<CalendarEvent> CreateCalendarEventsWithCertainDays(UserEvent userEvent, CertainDays days, WeekOfTheMonth weekOfTheMonth)
        {
            var firstCalendarEvent = CalendarEvent.ToCalendarEvent(userEvent);
            var events = new List<CalendarEvent>
            {
                firstCalendarEvent
            };
            var dateOfNextEvent = userEvent.Date;
            while (dateOfNextEvent < userEvent.LastDate)
            {
                dateOfNextEvent = dateOfNextEvent.AddDays(1);
                var daysMatches = CertainDayHelper.ShouldOccurOnThisDay(dateOfNextEvent.DayOfWeek, days);

                // If current day of the week is not chosen, continue
                if (!daysMatches) continue;

                // If no specific week is chosen, simply create event
                if (weekOfTheMonth == default)
                {
                    var nextCalendarEvent = CreateCalendarEvent(firstCalendarEvent, dateOfNextEvent);
                    events.Add(nextCalendarEvent);
                    continue;
                }

                // Specific week is chosen, should check if current day is on this week
                if (CertainDayHelper.IsOnGivenWeekOfMonth(weekOfTheMonth, dateOfNextEvent))
                {
                    var nextCalendarEvent = CreateCalendarEvent(firstCalendarEvent, dateOfNextEvent);
                    events.Add(nextCalendarEvent);
                }
            }
            return events;
        }

        private static IEnumerable<CalendarEvent> CreateCalendarEventsWithEvenOrOddDays(UserEvent userEvent, EvenOdd evenOdd)
        {
            var firstCalendarEvent = CalendarEvent.ToCalendarEvent(userEvent);
            var events = new List<CalendarEvent>
            {
                firstCalendarEvent
            };
            var dateOfNextEvent = userEvent.Date;
            while (dateOfNextEvent < userEvent.LastDate)
            {
                dateOfNextEvent = dateOfNextEvent.AddDays(1);
                if ((evenOdd == EvenOdd.Even && dateOfNextEvent.Day % 2 == 0)
                    || (evenOdd == EvenOdd.Odd && dateOfNextEvent.Day % 2 == 1))
                {
                    var nextCalendarEvent = CreateCalendarEvent(firstCalendarEvent, dateOfNextEvent);
                    events.Add(nextCalendarEvent);
                }
            }
            return events;
        }
    }
}
