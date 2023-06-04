using ApplicationCore.FileGenerators.Contracts;
using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using ApplicationCore.Profiles;
using ApplicationCore.Providers.Contracts;
using ApplicationCore.Repositories.Contracts;
using ApplicationCore.Services;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace Tests.Services
{
    public class UserEventServiceTest
    {
        private readonly Mock<IUserEventRepository> _mockUserEventRepo;
        private readonly Mock<IRecurrencyRuleRepository> _mockRecRuleRepo;
        private readonly Mock<IIcsFileGenerator> _mockIcsFileGenerator;
        private readonly Mock<IUserNameProvider> _mockUserNameProvider;

        // System under test
        private readonly IUserEventService _sut;

        public UserEventServiceTest()
        {
            _mockUserEventRepo = new();
            _mockRecRuleRepo = new();
            var profile = new AutomapperProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(config);
            _mockIcsFileGenerator = new();
            var mockLogger = new Mock<ILogger<IUserEventService>>();
            _mockUserNameProvider = new();
            _sut = new UserEventService(_mockUserEventRepo.Object, _mockRecRuleRepo.Object, mapper, _mockIcsFileGenerator.Object, mockLogger.Object, _mockUserNameProvider.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNull_WhenNoEvents()
        {
            // Arrange
            _mockUserEventRepo.Setup(x => x.GetAll(It.IsAny<Guid>()))
                .ReturnsAsync(() => null!);

            // Act
            var results = await _sut.GetUserEvents(It.IsAny<Guid>());

            // Assert
            results.ShouldBeNull();
        }

        [Fact]
        public async Task GetAll_ShouldReturnEvents_WhenExists()
        {
            // Arrange
            _mockUserEventRepo.Setup(x => x.GetAll(It.IsAny<Guid>()))
                .ReturnsAsync(TestData.GetUserEvents());

            // Act
            var results = await _sut.GetUserEvents(It.IsAny<Guid>());

            // Assert
            results.ShouldNotBeNull();
            results.Count().ShouldBe(2);
            results.ToList().ForEach(x => x.ShouldBeOfType<UserEvent>());
        }

        [Fact]
        public async Task GetById_ShouldReturnEvent_WhenFound()
        {
            // Arrange
            var testGuid1 = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();
            _mockUserEventRepo.Setup(x => x.GetById(testGuid1))
                .ReturnsAsync(TestData.GetUserEvents().ToList()[0]);
            _mockUserEventRepo.Setup(x => x.GetById(testGuid2))
                .ReturnsAsync(TestData.GetUserEvents().ToList()[1]);

            // Act            
            var testGuid = (new Random().Next(1, 3) % 2 == 0) ? testGuid1 : testGuid2;
            var result = await _sut.GetUserEventById(testGuid);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<UserEvent>();
        }              

        [Fact]
        public async Task AddInvitation_WhenAllOk_ShouldSaveInvitation()
        {
            // Arrange
            Guid eventId = Guid.NewGuid();
            Guid invitationId = Guid.NewGuid();
            string userName = "testuser";

            var userEvent = new UserEvent
            {
                Id = eventId,
                InvitationIds = new List<string>()
            };

            _mockUserEventRepo.Setup(x => x.GetById(eventId)).ReturnsAsync(userEvent);
            _mockUserEventRepo.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            await _sut.AddInvitationToUserEvent(eventId, invitationId, userName);

            // Assert
            _mockUserEventRepo.Verify(x => x.GetById(eventId), Times.Once);
            _mockUserEventRepo.Verify(x => x.SaveAsync(), Times.Once);
            _mockUserNameProvider.Verify(x => x.SetUserName(userName), Times.Once);
            Assert.Contains(invitationId.ToString(), userEvent.InvitationIds);
        }

        [Theory]
        [MemberData(nameof(ProvideTestData))]
        public async Task GetCalendarEvents_ShouldReturnManyEvents_WhenIsRecurring(UserEvent userEvent, int createdEventsCount)
        {
            // Arrange
            _mockUserEventRepo.Setup(x => x.GetAll(It.IsAny<Guid>()))
                .ReturnsAsync(new List<UserEvent> { userEvent });

            // Act
            var results = await _sut.GetCalendarEvents(It.IsAny<Guid>());

            // Assert
            results.Count().ShouldBe(createdEventsCount);
        }

        public static IEnumerable<object[]> ProvideTestData()
        {
            // Provide new Event and count of events, which should be created according to recurrency propery
            var startDateTime = new DateTime(2023, 6, 1, 12, 0, 0);

            // None
            yield return new object[] {
                new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Category = "Test",
                    Place = "Test",
                    StartTime = startDateTime,
                    EndTime = startDateTime.AddHours(1),
                    Date = startDateTime,
                    LastDate = startDateTime,
                    Description = "Test",
                    AdditionalInfo = "Test",
                    ImageUrl = "Test",
                    HasRecurrency = false
                },
                1
            };
            // Daily
            yield return new object[] {
                new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Category = "Test",
                    Place = "Test",
                    StartTime = startDateTime,
                    EndTime = startDateTime.AddHours(1),
                    Date = startDateTime,
                    LastDate = startDateTime.AddDays(7),
                    Description = "Test",
                    AdditionalInfo = "Test",
                    ImageUrl = "Test",
                    HasRecurrency = true,
                    RecurrencyRule = new RecurrencyRule
                    {
                        Recurrency = Recurrency.Daily
                    }
                },
                8
            };
            // Weekly
            yield return new object[] {
                new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Category = "Test",
                    Place = "Test",
                    StartTime = startDateTime,
                    EndTime = startDateTime.AddHours(1),
                    Date = startDateTime,
                    LastDate = startDateTime.AddMonths(1),
                    Description = "Test",
                    AdditionalInfo = "Test",
                    ImageUrl = "Test",
                    HasRecurrency = true,
                    RecurrencyRule = new RecurrencyRule
                    {
                        Recurrency = Recurrency.Weekly
                    }
                },
                6
            };
            // Monthly
            yield return new object[] {
                new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Category = "Test",
                    Place = "Test",
                    StartTime = startDateTime,
                    EndTime = startDateTime.AddHours(1),
                    Date = startDateTime,
                    LastDate = startDateTime.AddMonths(3),
                    Description = "Test",
                    AdditionalInfo = "Test",
                    ImageUrl = "Test",
                    HasRecurrency = true,
                    RecurrencyRule = new RecurrencyRule
                    {
                        Recurrency = Recurrency.Monthly
                    }
                },
                4
            };
            // Yearly
            yield return new object[] {
                new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Category = "Test",
                    Place = "Test",
                    StartTime = startDateTime,
                    EndTime = startDateTime.AddHours(1),
                    Date = startDateTime,
                    LastDate = startDateTime.AddYears(3),
                    Description = "Test",
                    AdditionalInfo = "Test",
                    ImageUrl = "Test",
                    RecurrencyRule = new RecurrencyRule
                    {
                        Recurrency = Recurrency.Yearly
                    }
                },
                4
            };
            // On even days
            yield return new object[] {
                new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Category = "Test",
                    Place = "Test",
                    StartTime = startDateTime,
                    EndTime = startDateTime.AddHours(1),
                    Date = startDateTime,
                    LastDate = startDateTime.AddDays(7),
                    Description = "Test",
                    AdditionalInfo = "Test",
                    ImageUrl = "Test",
                    HasRecurrency = true,
                    RecurrencyRule = new RecurrencyRule
                    {
                        EvenOdd = EvenOdd.Even
                    }
                },
                5
            };
            // On odd days
            yield return new object[] {
                new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Category = "Test",
                    Place = "Test",
                    StartTime = startDateTime,
                    EndTime = startDateTime.AddHours(1),
                    Date = startDateTime,
                    LastDate = startDateTime.AddDays(7),
                    Description = "Test",
                    AdditionalInfo = "Test",
                    ImageUrl = "Test",
                    HasRecurrency = true,
                    RecurrencyRule = new RecurrencyRule
                    {
                        EvenOdd = EvenOdd.Odd
                    }
                },
                4
            };
            // Every first monday and friday of month
            yield return new object[] {
                new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Category = "Test",
                    Place = "Test",
                    StartTime = startDateTime,
                    EndTime = startDateTime.AddHours(1),
                    Date = startDateTime,
                    LastDate = startDateTime.AddMonths(3),
                    Description = "Test",
                    AdditionalInfo = "Test",
                    ImageUrl = "Test",
                    HasRecurrency = true,
                    RecurrencyRule = new RecurrencyRule
                    {
                        DayOfWeek = (CertainDays)17,
                        WeekOfMonth = WeekOfTheMonth.First
                    }
                },
                6
            };
            // Every third thursday, saturday and sunday
            yield return new object[] {
                new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Category = "Test",
                    Place = "Test",
                    StartTime = startDateTime,
                    EndTime = startDateTime.AddHours(1),
                    Date = startDateTime,
                    LastDate = startDateTime.AddMonths(3),
                    Description = "Test",
                    AdditionalInfo = "Test",
                    ImageUrl = "Test",
                    HasRecurrency = true,
                    RecurrencyRule = new RecurrencyRule
                    {
                        DayOfWeek = (CertainDays)104,
                        WeekOfMonth = WeekOfTheMonth.Third
                    }
                },
                10
            };
            // Every last friday of month
            yield return new object[] {
                new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Category = "Test",
                    Place = "Test",
                    StartTime = startDateTime,
                    EndTime = startDateTime.AddHours(1),
                    Date = startDateTime,
                    LastDate = startDateTime.AddMonths(3),
                    Description = "Test",
                    AdditionalInfo = "Test",
                    ImageUrl = "Test",
                    HasRecurrency = true,
                    RecurrencyRule = new RecurrencyRule
                    {
                        DayOfWeek = (CertainDays)16,
                        WeekOfMonth = WeekOfTheMonth.Last
                    }
                },
                4
            };
            // On working days
            yield return new object[] {
                new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Category = "Test",
                    Place = "Test",
                    StartTime = startDateTime,
                    EndTime = startDateTime.AddHours(1),
                    Date = startDateTime,
                    LastDate = startDateTime.AddDays(14),
                    Description = "Test",
                    AdditionalInfo = "Test",
                    ImageUrl = "Test",
                    HasRecurrency = true,
                    RecurrencyRule = new RecurrencyRule
                    {
                        DayOfWeek = (CertainDays)31,
                    }
                },
                11
            };
            // On weekend
            yield return new object[] {
                new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Category = "Test",
                    Place = "Test",
                    StartTime = startDateTime,
                    EndTime = startDateTime.AddHours(1),
                    Date = startDateTime,
                    LastDate = startDateTime.AddDays(14),
                    Description = "Test",
                    AdditionalInfo = "Test",
                    ImageUrl = "Test",
                    HasRecurrency = true,
                    RecurrencyRule = new RecurrencyRule
                    {
                        DayOfWeek = (CertainDays)96,
                    }
                },
                5
            };
        }
    }
}
