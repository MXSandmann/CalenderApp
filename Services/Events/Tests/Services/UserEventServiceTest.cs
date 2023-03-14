using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using ApplicationCore.Profiles;
using ApplicationCore.Repositories.Contracts;
using ApplicationCore.Services;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Moq;
using Shouldly;

namespace Tests.Services
{
    public class UserEventServiceTest
    {
        private readonly Mock<IUserEventRepository> _userEventRepoMock;
        private readonly Mock<IRecurrencyRuleRepository> _recRuleRepoMock;

        // System under test
        private readonly IUserEventService _sut;

        public UserEventServiceTest()
        {
            _userEventRepoMock = new Mock<IUserEventRepository>();
            _recRuleRepoMock = new Mock<IRecurrencyRuleRepository>();
            var profile = new AutomapperProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(config);
            _sut = new UserEventService(_userEventRepoMock.Object, _recRuleRepoMock.Object, mapper);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNull_WhenNoEvents()
        {
            // Arrange
            _userEventRepoMock.Setup(x => x.GetAll())
                .ReturnsAsync(() => null!);

            // Act
            var results = await _sut.GetUserEvents();

            // Assert
            results.ShouldBeNull();
        }

        [Fact]
        public async Task GetAll_ShouldReturnEvents_WhenExists()
        {
            // Arrange
            _userEventRepoMock.Setup(x => x.GetAll())
                .ReturnsAsync(TestData.GetUserEvents());

            // Act
            var results = await _sut.GetUserEvents();

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
            _userEventRepoMock.Setup(x => x.GetById(testGuid1))
                .ReturnsAsync(TestData.GetUserEvents().ToList()[0]);
            _userEventRepoMock.Setup(x => x.GetById(testGuid2))
                .ReturnsAsync(TestData.GetUserEvents().ToList()[1]);

            // Act            
            var testGuid = (new Random().Next(1, 3) % 2 == 0) ? testGuid1 : testGuid2;
            var result = await _sut.GetUserEventById(testGuid);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<UserEvent>();
        }

        [Theory]
        [MemberData(nameof(ProvideTestData))]
        public async Task GetCalendarEvents_ShouldReturnManyEvents_WhenIsRecurring(UserEvent userEvent, int createdEventsCount)
        {
            // Arrange
            _userEventRepoMock.Setup(x => x.GetAll())
                .ReturnsAsync(new List<UserEvent> { userEvent });

            // Act
            var results = await _sut.GetCalendarEvents();

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
