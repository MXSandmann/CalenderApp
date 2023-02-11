﻿using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Shouldly;
using System.Net;
using System.Runtime.CompilerServices;
using WebUI.Clients;
using WebUI.Clients.Contracts;
using WebUI.Models;
using WebUI.Models.Dtos;
using WebUI.Models.Enums;

namespace Tests.Clients
{
    public class EventsClientTest
    {
        private readonly HttpClient _mockClient;
        private readonly MockHttpMessageHandler _mockHttpHandler;
        private readonly Mock<ILogger<IEventsClient>> _mockLogger;

        private readonly IEventsClient _sut;

        public EventsClientTest()
        {
            _mockHttpHandler = new MockHttpMessageHandler();
            _mockClient = new HttpClient(_mockHttpHandler)
            {
                BaseAddress = new Uri("http://random")
            };
            _mockLogger = new Mock<ILogger<IEventsClient>>();
            _sut = new EventsClient(_mockClient, _mockLogger.Object);
        }

        [Fact]
        public async Task AddNewUserEvent_ShouldReturnDto_WhenAllOk()
        {
            // Arrange
            var userEventDto = TestData.GetUserEventDtos().First();
            var recurrencyRuleDto = TestData.GetRecurrencyRuleDto(Recurrency.None);
            _mockHttpHandler.When(HttpMethod.Post, "/api/Events/Create").Respond(HttpStatusCode.OK, new StringContent(GetSingleUserEventDtoOkContent()));            

            // Act
            var dto = await _sut.AddNewUserEvent(userEventDto, recurrencyRuleDto);

            // Assert
            dto.ShouldBeOfType<UserEventDto>();
            dto.ShouldNotBeNull();
            dto.RecurrencyRule.ShouldNotBeNull();
        }

        [Fact]
        public async Task AddNewUserEvent_ShouldThrow_WhenNotOk()
        {
            // Arrange
            var userEventDto = TestData.GetUserEventDtos().First();
            var recurrencyRuleDto = TestData.GetRecurrencyRuleDto(Recurrency.None);
            _mockHttpHandler.When(HttpMethod.Post, "/api/Events/Create").Respond(HttpStatusCode.NotFound);

            // Act
            var func = async () => await _sut.AddNewUserEvent(userEventDto, recurrencyRuleDto);

            // Assert
            await func.ShouldThrowAsync<HttpRequestException>();            
        }

        [Fact]
        public async Task GetCalendarEvents_ShouldGetEvents_WhenAllOk()
        {
            // Arrange
            _mockHttpHandler.When("/api/Home").Respond(HttpStatusCode.OK, new StringContent(GetCalendarEventsOkContent()));

            // Act
            var results = await _sut.GetCalendarEvents();

            // Assert
            results.ShouldBeAssignableTo<IEnumerable<CalendarEvent>>();
            results.ShouldNotBeEmpty();
            results.Count().ShouldBe(2);
        }

        [Fact]
        public async Task GetCalendarEvents_ShouldGetEmptyList_WhenNotOk()
        {
            // Arrange
            _mockHttpHandler.When("/api/Home").Respond(HttpStatusCode.OK, new StringContent(GetEmptyCalendarEventsContent()));

            // Act
            var results = await _sut.GetCalendarEvents();

            // Assert
            results.ShouldBeAssignableTo<IEnumerable<CalendarEvent>>();
            results.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetUserEventById_ShouldReturnEvent_WhenAllOK()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockHttpHandler.When($"/api/Events/{id}").Respond(HttpStatusCode.OK, new StringContent(GetSingleUserEventDtoOkContent()));

            // Act
            var result = await _sut.GetUserEventById(id);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeAssignableTo<UserEventDto>();
        }

        [Fact]
        public async Task GetUserEventById_ShouldReturnEvent_WhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockHttpHandler.When($"/api/Events/{id}").Respond(HttpStatusCode.OK, new StringContent(GetUserEventDtoNullContent()));

            // Act
            var func = async () => await _sut.GetUserEventById(id);

            // Assert
            await func.ShouldThrowAsync<ArgumentNullException>();            
        }

        [Theory]
        [InlineData("")]
        [InlineData("Place")]
        [InlineData("Category")]
        [InlineData("Time")]
        public async Task GetUserEvents_ShouldReturnSortedEvents_WhenOk(string sortBy)
        {
            // Arrange            
            _mockHttpHandler.When("/api/Events").Respond(HttpStatusCode.OK, new StringContent(GetUserEventDtosOkContent()));
            var dict = new Dictionary<string, Action<IEnumerable<UserEventDto>>>
            {
                { "", (seq) => CheckOrderByTime(seq) },
                { "Time", (seq) => CheckOrderByTime(seq) },
                { "Category", (seq) => CheckOrderByCategory(seq) },
                { "Place", (seq) => CheckOrderByPlace(seq) }
            };

            // Act
            var results = await _sut.GetUserEvents(sortBy);

            // Assert
            var act = dict[sortBy];
            act(results);

        }

        [Fact]
        public async Task GetUserEvents_ShouldReturnEmptyList_WhenNoEvents()
        {
            // Arrange
            _mockHttpHandler.When("/api/Events").Respond(HttpStatusCode.OK, new StringContent(GetEmptyUserEventDtosContent()));

            // Act
            var results = await _sut.GetUserEvents(string.Empty);

            // Assert
            results.ShouldBeEmpty();
            results.ShouldBeAssignableTo<IEnumerable<UserEventDto>>();

        }

        [Fact]
        public async Task RemoveUserEvent_ShouldThrow_WhenNotFound()
        {
            // Arrange
            var id = Guid.Empty;
            _mockHttpHandler.When($"/api/Events/Remove/{id}").Respond(HttpStatusCode.NotFound);

            // Act
            var act = () => _sut.RemoveUserEvent(id);

            // Assert
            await act.ShouldThrowAsync<HttpRequestException>();
        }

        [Fact]
        public async Task UpdateUserEvent_ShouldReturnUserEvent_WhenAllOk()
        {
            // Arrange
            var userEventDto = TestData.GetUserEventDtos().First();
            var recurrencyRule = TestData.GetRecurrencyRuleDto(Recurrency.Daily);
            userEventDto.HasRecurrency = true;
            userEventDto.RecurrencyRule = recurrencyRule;
            _mockHttpHandler.When("/api/Events/Update").Respond(HttpStatusCode.OK, new StringContent(JsonConvert.SerializeObject(userEventDto)));

            // Act
            var result = await _sut.UpdateUserEvent(userEventDto, recurrencyRule);

            // Assert
            result.ShouldBeOfType<UserEventDto>();
            result.RecurrencyRule.ShouldNotBeNull();
            result.RecurrencyRule.ShouldBeOfType<RecurrencyRuleDto>();
        }

        [Fact]
        public async Task UpdateUserEvent_ShouldThrow_WhenNotFound()
        {
            // Arrange
            var userEventDto = TestData.GetUserEventDtos().First();
            var recurrencyRule = TestData.GetRecurrencyRuleDto(Recurrency.Daily);
            userEventDto.HasRecurrency = true;
            userEventDto.RecurrencyRule = recurrencyRule;
            _mockHttpHandler.When("/api/Events/Update").Respond(HttpStatusCode.NotFound);

            // Act
            var act = () => _sut.UpdateUserEvent(userEventDto, recurrencyRule);

            // Assert
            await act.ShouldThrowAsync<HttpRequestException>();
        }

        private static void CheckOrderByCategory(IEnumerable<UserEventDto> results)
        {
            var expected = TestData.GetUserEventDtos().OrderBy(x => x.Category);
            foreach (var (result, exp) in results.Zip(expected))
            {
                Assert.Equal(result.Category, exp.Category);
            }
        }

        private static void CheckOrderByPlace(IEnumerable<UserEventDto> results)
        {
            var expected = TestData.GetUserEventDtos().OrderBy(x => x.Place);
            foreach (var (result, exp) in results.Zip(expected))
            {
                Assert.Equal(result.Place, exp.Place);
            }
        }

        private static void CheckOrderByTime(IEnumerable<UserEventDto> results)
        {
            var expected = TestData.GetUserEventDtos().OrderBy(x => x.Date).ThenBy(x => x.StartTime);
            foreach (var (result, exp) in results.Zip(expected))
            {
                Assert.Equal(result.Date, exp.Date);
                Assert.Equal(result.StartTime, exp.StartTime);
            }
        }

        private static string GetSingleUserEventDtoOkContent()
        {
            var content = TestData.GetUserEventDtos().First();
            content.RecurrencyRule = TestData.GetRecurrencyRuleDto(Recurrency.None);
            return JsonConvert.SerializeObject(content);
        }

        private static string GetUserEventDtosOkContent()
        {
            var content = TestData.GetUserEventDtos();            
            return JsonConvert.SerializeObject(content);
        }
        private static string GetUserEventDtoNullContent()
        {
            var content = new { Id = Guid.Empty };                
            return JsonConvert.SerializeObject(content);
        }

        private static string GetCalendarEventsOkContent()
        {
            var content = TestData.GetCalendarEvents();          
            return JsonConvert.SerializeObject(content);
        }

        private static string GetEmptyCalendarEventsContent()
        {
            var content = Enumerable.Empty<CalendarEvent>();
            return JsonConvert.SerializeObject(content);
        }

        private static string GetEmptyUserEventDtosContent()
        {
            var content = Enumerable.Empty<UserEventDto>();
            return JsonConvert.SerializeObject(content);
        }
    }
}
