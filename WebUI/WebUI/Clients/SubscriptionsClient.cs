using Newtonsoft.Json;
using WebUI.Clients.Contracts;
using WebUI.Models.Dtos;

namespace WebUI.Clients
{
    public class SubscriptionsClient : ISubscriptionsClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ISubscriptionsClient> _logger;


        public SubscriptionsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<NotificationDto> AddNotification(NotificationDto notificationDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Notifications", notificationDto);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogInformation("--> Http request on SubsciptionsService unsuccessful, status code: {responseMessage.StatusCode}", response.StatusCode);
                throw new HttpRequestException($"Error during creating an event: {response.StatusCode}");
            }
            var content = await response.Content.ReadAsStringAsync();
            var newNotification = JsonConvert.DeserializeObject<NotificationDto>(content);
            ArgumentNullException.ThrowIfNull(newNotification);
            return newNotification;
        }

        public async Task<SubscriptionDto> AddSubscription(SubscriptionDto subscriptionDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Subscriptions", subscriptionDto);
            if(!response.IsSuccessStatusCode)
            {
                _logger.LogInformation("--> Http request on SubsciptionsService unsuccessful, status code: {responseMessage.StatusCode}", response.StatusCode);
                throw new HttpRequestException($"Error during creating an event: {response.StatusCode}");
            }
            var content = await response.Content.ReadAsStringAsync();
            var newSubscription = JsonConvert.DeserializeObject<SubscriptionDto>(content);
            ArgumentNullException.ThrowIfNull(newSubscription);
            return newSubscription;
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptions()
        {
            var subscriptions = await _httpClient.GetFromJsonAsync<IEnumerable<SubscriptionDto>>("Subscriptions");
            if(subscriptions == null || !subscriptions.Any()) return Enumerable.Empty<SubscriptionDto>();
            return subscriptions;

        }

        public Task<IEnumerable<SubscriptionDto>> GetSubscriptionForEvent(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveSubscriptionForEvent(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public Task<SubscriptionDto> UpdateSubscription(SubscriptionDto subscriptionDto)
        {
            throw new NotImplementedException();
        }
    }
}
