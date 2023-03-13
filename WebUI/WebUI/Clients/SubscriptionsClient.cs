using Newtonsoft.Json;
using WebUI.Clients.Contracts;
using WebUI.Models.Dtos;

namespace WebUI.Clients
{
    public class SubscriptionsClient : ISubscriptionsClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ISubscriptionsClient> _logger;


        public SubscriptionsClient(HttpClient httpClient, ILogger<ISubscriptionsClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
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
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogInformation("--> Http request on SubsciptionsService unsuccessful, status code: {responseMessage.StatusCode}", response.StatusCode);
                throw new HttpRequestException($"Error during creating an event: {response.StatusCode}");
            }
            var content = await response.Content.ReadAsStringAsync();
            var newSubscription = JsonConvert.DeserializeObject<SubscriptionDto>(content);
            ArgumentNullException.ThrowIfNull(newSubscription);
            return newSubscription;
        }

        public async Task<IEnumerable<UserActivityRecordDto>> GetAllActivities()
        {
            var results = await _httpClient.GetFromJsonAsync<IEnumerable<UserActivityRecordDto>>("Activities");
            if (results == null)
                return Enumerable.Empty<UserActivityRecordDto>();
            return results;
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptions()
        {
            var subscriptions = await _httpClient.GetFromJsonAsync<IEnumerable<SubscriptionDto>>("Subscriptions");
            if (subscriptions == null || !subscriptions.Any()) return Enumerable.Empty<SubscriptionDto>();
            return subscriptions;
        }

        public async Task<SubscriptionDto> GetSubscriptionById(Guid id)
        {
            var subscription = await _httpClient.GetFromJsonAsync<SubscriptionDto>($"Subscriptions/{id}");
            if (subscription?.SubscriptionId == null || subscription.SubscriptionId == Guid.Empty)
                throw new ArgumentNullException(nameof(subscription), "Subscription Id is null or empty");
            return subscription;
        }

        public async Task RemoveSubscription(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"Subscriptions/Remove/{id}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogInformation("--> Http request on SubscriptionsService unsuccessful, status code: {response.StatusCode}", response.StatusCode);
                throw new HttpRequestException($"Error during removing a subscription: {response.StatusCode}");
            }
            return;
        }

        public async Task<SubscriptionDto> UpdateSubscription(SubscriptionDto subscriptionDto)
        {
            var response = await _httpClient.PutAsJsonAsync("Subscriptions/Update", subscriptionDto);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogInformation("--> Http request on SubscriptionsService unsuccessful, status code: {response.StatusCode}", response.StatusCode);
                throw new HttpRequestException($"Error during updating a subscription: {response.StatusCode}");
            }
            var content = await response.Content.ReadAsStringAsync();
            var newSubscription = JsonConvert.DeserializeObject<SubscriptionDto>(content);
            ArgumentNullException.ThrowIfNull(newSubscription);
            return newSubscription;
        }
    }
}
