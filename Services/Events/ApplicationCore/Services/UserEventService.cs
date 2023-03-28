using ApplicationCore.Extensions;
using ApplicationCore.Models;
using ApplicationCore.Models.Dtos;
using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using ApplicationCore.Repositories.Contracts;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using ApplicationCore.FileGenerators.Contracts;

namespace ApplicationCore.Services
{
    public class UserEventService : IUserEventService
    {
        private readonly IUserEventRepository _userEventRepository;
        private readonly IRecurrencyRuleRepository _recurrencyRuleRepository;
        private readonly IMapper _mapper;
        private readonly IIcsFileGenerator _icsFileGenerator;

        public UserEventService(IUserEventRepository repository, IRecurrencyRuleRepository recurrencyRuleRepository, IMapper mapper, IIcsFileGenerator icsFileGenerator)
        {
            _userEventRepository = repository;
            _recurrencyRuleRepository = recurrencyRuleRepository;
            _mapper = mapper;
            _icsFileGenerator = icsFileGenerator;
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
            return await _userEventRepository.GetById(newUserEventId);
        }

        public async Task<UserEvent> GetUserEventById(Guid id)
        {
            return await _userEventRepository.GetById(id);
        }

        public async Task<IEnumerable<UserEvent>> GetUserEvents()
        {
            return await _userEventRepository.GetAll();
        }

        public async Task RemoveUserEvent(Guid id)
        {
            var userEventToDelete = await _userEventRepository.GetById(id);
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
            var userEvents = await _userEventRepository.GetAll();

            if (!userEvents.Any()) return Enumerable.Empty<CalendarEvent>();

            var calendarEvents = new List<CalendarEvent>();

            foreach (var userEvent in userEvents)
            {
                var rule = userEvent.RecurrencyRule;

                // 1. Check if has recurrency
                if (rule == null)
                {
                    calendarEvents.Add(_mapper.Map<CalendarEvent>(userEvent));
                    continue;
                }

                // 2. Check standard recurrency
                if (rule.Recurrency != Recurrency.None)
                    calendarEvents.AddRange(CreateCalendarEventsWithStandardRecurrency(userEvent, rule.Recurrency, _mapper));

                // 3. Check if certain day of week is set
                if (rule.DayOfWeek != default)
                    calendarEvents.AddRange(CreateCalendarEventsWithCertainDays(userEvent, rule.DayOfWeek, rule.WeekOfMonth, _mapper));

                // 4. Check if even or odd day
                if (rule.EvenOdd != default)
                    calendarEvents.AddRange(CreateCalendarEventsWithEvenOrOddDays(userEvent, rule.EvenOdd, _mapper));
            }
            return calendarEvents;
        }

        private static IEnumerable<CalendarEvent> CreateCalendarEventsWithStandardRecurrency(UserEvent userEvent, Recurrency recurrency, IMapper mapper)
        {
            var calendarEvents = new List<CalendarEvent>();
            var events = CompleteEventsForPeriod(userEvent, recurrency, mapper);
            calendarEvents.AddRange(events);
            return calendarEvents;
        }

        private static List<CalendarEvent> CompleteEventsForPeriod(UserEvent userEvent, Recurrency recurrency, IMapper mapper)
        {
            var firstCalendarEvent = mapper.Map<CalendarEvent>(userEvent);
            var events = new List<CalendarEvent>
            {
                firstCalendarEvent
            };
            var dateOfNextEvent = userEvent.Date;
            while (dateOfNextEvent < userEvent.LastDate)
            {
                dateOfNextEvent = dateOfNextEvent.AddTimePeriod(recurrency);
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

        private static IEnumerable<CalendarEvent> CreateCalendarEventsWithCertainDays(UserEvent userEvent, CertainDays days, WeekOfTheMonth weekOfTheMonth, IMapper mapper)
        {
            var firstCalendarEvent = mapper.Map<CalendarEvent>(userEvent);
            var events = new List<CalendarEvent>
            {
                firstCalendarEvent
            };
            var dateOfNextEvent = userEvent.Date;
            while (dateOfNextEvent < userEvent.LastDate)
            {
                dateOfNextEvent = dateOfNextEvent.AddDays(1);
                var daysMatches = days.ShouldOccurOnThisDay(dateOfNextEvent.DayOfWeek);

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
                if (weekOfTheMonth.IsOnGivenWeekOfMonth(dateOfNextEvent))
                {
                    var nextCalendarEvent = CreateCalendarEvent(firstCalendarEvent, dateOfNextEvent);
                    events.Add(nextCalendarEvent);
                }
            }
            return events;
        }

        private static IEnumerable<CalendarEvent> CreateCalendarEventsWithEvenOrOddDays(UserEvent userEvent, EvenOdd evenOdd, IMapper mapper)
        {
            var firstCalendarEvent = mapper.Map<CalendarEvent>(userEvent);
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

        public async Task<Dictionary<Guid, string>> GetEventNames(IEnumerable<Guid> eventIds)
        {
            return await _userEventRepository.GetEventNames(eventIds);
        }

        public async Task<PaginationResponse<UserEventDto>> SearchUserEvents(string entry, int limit, int offset)
        {
            var (results, count) = await _userEventRepository.SearchUserEvents(entry, limit, offset);
            var resultDtos = _mapper.Map<IEnumerable<UserEventDto>>(results);
            return new PaginationResponse<UserEventDto>(resultDtos, count);
        }

        public async Task<string> DownloadICSFile(Guid eventId)
        {
            // Get the event first
            var userEvent = await _userEventRepository.GetById(eventId);
            ArgumentNullException.ThrowIfNull(userEvent);

            var icsFileString = _icsFileGenerator.Generate(userEvent);            
            return icsFileString;
        }
    }
}
