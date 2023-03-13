using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Shouldly;
using System.Net;
using WebUI.Clients;
using WebUI.Clients.Contracts;
using WebUI.Models.Dtos;

namespace Tests.Clients
{
    public class SubscriptionsClientTest
    {
        private readonly MockHttpMessageHandler _mockHttpHandler;
        private readonly ISubscriptionsClient _sut;

        public SubscriptionsClientTest()
        {
            _mockHttpHandler = new MockHttpMessageHandler();
            var mockClient = new HttpClient(_mockHttpHandler)
            {
                BaseAddress = new Uri("http://random")
            };
            var mockLogger = new Mock<ILogger<ISubscriptionsClient>>();
            _sut = new SubscriptionsClient(mockClient, mockLogger.Object);
        }

        [Fact]
        public async Task AddNotification_ShouldReturnNewNotification_WhenAllOk()
        {
            // Arrange
            var dto = TestData.GetNotificationDto();
            _mockHttpHandler.When("/Notifications")
                .Respond(HttpStatusCode.OK, new StringContent(JsonConvert.SerializeObject(dto)));

            // Act
            var result = await _sut.AddNotification(dto);

            // Assert
            result.ShouldBeOfType<NotificationDto>();
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task AddNotification_ShouldThrow_WhenNotOk()
        {
            // Arrange
            var dto = TestData.GetNotificationDto();
            _mockHttpHandler.When("/Notifications")
                .Respond(HttpStatusCode.NotFound);

            // Act
            var func = async () => await _sut.AddNotification(dto);

            // Assert
            await func.ShouldThrowAsync<HttpRequestException>();
        }

        [Fact]
        public async Task AddSubscription_ShouldReturnNewSubscription_WhenAllOk()
        {
            // Arrange
            var dto = TestData.GetSubscriptionDtos().First();
            _mockHttpHandler.When("/Subscriptions")
                .Respond(HttpStatusCode.OK, new StringContent(JsonConvert.SerializeObject(dto)));

            // Act
            var result = await _sut.AddSubscription(dto);

            // Assert
            result.ShouldBeOfType<SubscriptionDto>();
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task AddSubscription_ShouldThrow_WhenNotOk()
        {
            // Arrange
            var dto = TestData.GetSubscriptionDtos().First();
            _mockHttpHandler.When("/Subscriptions")
                .Respond(HttpStatusCode.NotFound);

            // Act
            var func = async () => await _sut.AddSubscription(dto);

            // Assert
            await func.ShouldThrowAsync<HttpRequestException>();
        }

        [Fact]
        public async Task GetAllSubscriptions_ShouldReturnSubscriptions_WhenAllOk()
        {
            // Arrange
            var dtos = TestData.GetSubscriptionDtos();
            _mockHttpHandler.When("/Subscriptions")
                .Respond(HttpStatusCode.OK, new StringContent(JsonConvert.SerializeObject(dtos)));

            // Act
            var results = await _sut.GetAllSubscriptions();

            // Assert
            results.ShouldBeAssignableTo<IEnumerable<SubscriptionDto>>();
            results.ShouldNotBeEmpty();
            results.Count().ShouldBe(2);
        }

        [Fact]
        public async Task GetAllSubscriptions_ShouldThrow_WhenNotOk()
        {
            // Arrange            
            _mockHttpHandler.When("/Subscriptions")
                .Respond(HttpStatusCode.OK, new StringContent(JsonConvert.SerializeObject(new List<SubscriptionDto>())));

            // Act
            var results = await _sut.GetAllSubscriptions();

            // Assert
            results.ShouldBeAssignableTo<IEnumerable<SubscriptionDto>>();
            results.ShouldBeEmpty();
        }


        [Fact]
        public async Task GetSubscriptionById_ShouldReturnSubscription_WhenAllOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = TestData.GetSubscriptionDtos().First();
            _mockHttpHandler.When($"/Subscriptions/{id}")
                .Respond(HttpStatusCode.OK, new StringContent(JsonConvert.SerializeObject(dto)));

            // Act
            var results = await _sut.GetSubscriptionById(id);

            // Assert
            results.ShouldBeAssignableTo<SubscriptionDto>();
            results.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetSubscriptionById_ShouldThrow_WhenNotOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockHttpHandler.When($"/Subscriptions/{id}")
                .Respond(HttpStatusCode.OK, new StringContent(JsonConvert.SerializeObject(new SubscriptionDto())));

            // Act
            var func = async () => await _sut.GetSubscriptionById(id);

            // Assert
            await func.ShouldThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateSubscription_ShouldReturnUpdatedSubscription_WhenAllOk()
        {
            // Arrange
            _mockHttpHandler.When("/Subscriptions/Update").
                Respond(HttpStatusCode.OK, new StringContent(JsonConvert.SerializeObject(new SubscriptionDto())));

            // Act
            var result = await _sut.UpdateSubscription(new SubscriptionDto());

            // Assert
            result.ShouldBeOfType<SubscriptionDto>();
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task UpdateSubscription_ShouldThrow_WhenNotOk()
        {
            // Arrange
            _mockHttpHandler.When("/Subscriptions/Update").
                Respond(HttpStatusCode.NotFound);

            // Act
            var func = async () => await _sut.UpdateSubscription(new SubscriptionDto());

            // Assert
            await func.ShouldThrowAsync<HttpRequestException>();
        }
    }
}
