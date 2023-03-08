﻿using ApplicationCore.Models;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using WebUI.Clients.Contracts;
using WebUI.Models;
using WebUI.Models.Dtos;

namespace WebUI.Clients
{
    public class EventsClient : IEventsClient
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger<IEventsClient> _logger;

        public EventsClient(HttpClient httpClient, ILogger<IEventsClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<UserEventDto> AddNewUserEvent(UserEventDto userEventDto, RecurrencyRuleDto recurrencyRuleDto)
        {
            if(userEventDto.HasRecurrency)
                userEventDto.RecurrencyRule = recurrencyRuleDto;

            var responseMessage = await _httpClient.PostAsJsonAsync("Events/Create", userEventDto);
            if (!responseMessage.IsSuccessStatusCode)
            {
                _logger.LogInformation("--> Http request on EventsService unsuccessful, status code: {responseMessage.StatusCode}", responseMessage.StatusCode);
                throw new HttpRequestException($"Error during creating an event: {responseMessage.StatusCode}");
            }
                
            var content =  await responseMessage.Content.ReadAsStringAsync();
            var newUserEvent = JsonConvert.DeserializeObject<UserEventDto>(content);
            ArgumentNullException.ThrowIfNull(newUserEvent);
            return newUserEvent;
        }

        public async Task<IEnumerable<CalendarEvent>> GetCalendarEvents()
        {
            var calendarEvents = await _httpClient.GetFromJsonAsync<IEnumerable<CalendarEvent>>("Home");
            if(calendarEvents == null
                || !calendarEvents.Any())
                return Enumerable.Empty<CalendarEvent>();
            return calendarEvents;
        }

        public async Task<UserEventDto> GetUserEventById(Guid id)
        {
            var userEvent = await _httpClient.GetFromJsonAsync<UserEventDto>($"Events/{id}");
            if (userEvent?.Id == null || userEvent.Id == Guid.Empty)
                throw new ArgumentNullException(nameof(userEvent), "UserEvent Id is null or empty");
            return userEvent;
        }

        public async Task AddUserEventNamesForSubscriptions(IEnumerable<SubscriptionDto> subscriptions)
        {
            var subscriptionIds = subscriptions.Select(x => x.EventId).ToList();
            var responseMessage = await _httpClient.PostAsJsonAsync("Events/EventNames", subscriptionIds);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var dict = JsonConvert.DeserializeObject<Dictionary<Guid, string>>(content);
            ArgumentNullException.ThrowIfNull(dict);
            MergeSubscriptionsWithEventNames(dict, subscriptions);            
        }

        private static void MergeSubscriptionsWithEventNames(Dictionary<Guid, string> dict, IEnumerable<SubscriptionDto> subscriptions)
        {
            foreach (var subscr in subscriptions)
            {
                if(!dict.TryGetValue(subscr.EventId, out var eventName)) continue;
                subscr.EventName = eventName;
            }            
        }

        public async Task<IEnumerable<UserEventDto>> GetUserEvents(string sortBy)
        {
            var events = await _httpClient.GetFromJsonAsync<IEnumerable<UserEventDto>>("Events");
            if(events == null || !events.Any())
                return Enumerable.Empty<UserEventDto>();

            // Sort items according to the parameter sortBy
            if(events == null)
                return Enumerable.Empty<UserEventDto>();
            if (sortBy != null
                && sortBy.Equals("Place"))
                return events.OrderBy(x => x.Place);
            if(sortBy != null
                && sortBy.Equals("Category"))
                return events.OrderBy(x => x.Category);
            return events.OrderBy(x => x.Date).ThenBy(x => x.StartTime);
        }

        public async Task RemoveUserEvent(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"Events/Remove/{id}");
            if(!response.IsSuccessStatusCode)
            {
                _logger.LogInformation("--> Http request on EventsService unsuccessful, status code: {responseMessage.StatusCode}", response.StatusCode);
                throw new HttpRequestException($"Error during removing an event: {response.StatusCode}");
            }
            return;
        }

        public async Task<UserEventDto> UpdateUserEvent(UserEventDto userEventDto, RecurrencyRuleDto recurrencyRuleDto)
        {
            if (userEventDto.HasRecurrency)
                userEventDto.RecurrencyRule = recurrencyRuleDto;

            var response = await _httpClient.PutAsJsonAsync("Events/Update", userEventDto);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogInformation("--> Http request on EventsService unsuccessful, status code: {responseMessage.StatusCode}", response.StatusCode);
                throw new HttpRequestException($"Error during updating an event: {response.StatusCode}");
            }
            var content = await response.Content.ReadAsStringAsync();
            var newUserEvent = JsonConvert.DeserializeObject<UserEventDto>(content);
            ArgumentNullException.ThrowIfNull(newUserEvent);
            return newUserEvent;
        }

        public async Task<(IEnumerable<UserEventDto>, int)> GetSearchResults(SearchUserEventsDto searchDto)
        {            
            var queryDict = new Dictionary<string, string>
            {
                { "entry", searchDto.Entry },
                { "limit", searchDto.Limit.ToString() },
                { "offset", searchDto.Offset.ToString() }
            };

            var url = QueryHelpers.AddQueryString("Events/Search", queryDict!);

            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<UserEventDto>>(url);

            _logger.LogInformation("Received search results: {value}", JsonConvert.SerializeObject(result));

            if(result?.Data == null)
                return (Enumerable.Empty<UserEventDto>(), 0);

            return (result.Data, result.Count);
        }
    }
}