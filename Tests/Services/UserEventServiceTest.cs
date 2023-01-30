using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using ApplicationCore.Repositories.Contracts;
using ApplicationCore.Services;
using ApplicationCore.Services.Contracts;
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
            _sut = new UserEventService(_userEventRepoMock.Object, _recRuleRepoMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNull_WhenNoEvents()
        {
            // Arrange
            _userEventRepoMock.Setup(x => x.GetAll(It.IsAny<string>()))
                .ReturnsAsync(() => null!);

            // Act
            var results = await _sut.GetUserEvents(string.Empty);

            // Assert
            results.ShouldBeNull();
        }

        [Fact]
        public async Task GetAll_ShouldReturnEvents_WhenExists()
        {
            // Arrange
            _userEventRepoMock.Setup(x => x.GetAll(It.IsAny<string>()))
                .ReturnsAsync(TestData.GetUserEvents());

            // Act
            var results = await _sut.GetUserEvents(string.Empty);

            // Assert
            results.ShouldNotBeNull();
            results.Count().ShouldBe(2);           
            results.ToList().ForEach(x => x.ShouldBeOfType<UserEvent>());
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            _userEventRepoMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(() => null!);

            // Act
            var func = async () => await _sut.GetUserEventById(Guid.NewGuid());

            // Assert
            await func.ShouldThrowAsync<ArgumentNullException>();
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

        [Fact]
        public async Task Create_ShouldThrow_WhenIsRecurringIsInvalid()
        {
            // Arrange
            var userEvent = TestData.GetUserEvents().First();
            var recurrencyRule = TestData.GetRecurrencyRule((Recurrency)99);            
            _userEventRepoMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<UserEvent>>()))
                .Verifiable();

            // Act
            var func = async () => await _sut.AddNewUserEvent(userEvent, recurrencyRule);

            // Assert
            await func.ShouldThrowAsync<ArgumentException>();
        }

        [Theory]
        [MemberData(nameof(ProvideTestData))]
        public async Task GetCalendarEvents_ShouldReturnManyEvents_WhenIsRecurring(UserEvent userEvent, int createdEventsCount)
        {
            // Arrange
            _userEventRepoMock.Setup(x => x.GetAll(It.IsAny<string>()))
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
                    HasRecurrency = YesNo.No                    
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
                    HasRecurrency = YesNo.Yes,
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
                    HasRecurrency = YesNo.Yes,
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
                    HasRecurrency = YesNo.Yes,
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
                    HasRecurrency = YesNo.Yes,
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
                    HasRecurrency = YesNo.Yes,
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
                    HasRecurrency = YesNo.Yes,
                    RecurrencyRule = new RecurrencyRule
                    {
                        CertainDays = 17,
                        WeekOfMonth = WeekOfTheMonth.First
                    }
                },
                8
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
                    HasRecurrency = YesNo.Yes,
                    RecurrencyRule = new RecurrencyRule
                    {
                        CertainDays = 104,
                        WeekOfMonth = WeekOfTheMonth.Third
                    }
                },
                10
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
                    HasRecurrency = YesNo.Yes,
                    RecurrencyRule = new RecurrencyRule
                    {
                        CertainDays = 31,                        
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
                    HasRecurrency = YesNo.Yes,
                    RecurrencyRule = new RecurrencyRule
                    {
                        CertainDays = 96,
                    }
                },
                5
            };
        }
    }
}
