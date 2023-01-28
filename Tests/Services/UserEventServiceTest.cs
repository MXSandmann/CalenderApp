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
        private readonly Mock<IUserEventRepository> _repoMock;

        // System under test
        private readonly IUserEventService _sut;

        public UserEventServiceTest()
        {
            _repoMock= new Mock<IUserEventRepository>();
            _sut = new UserEventService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNull_WhenNoEvents()
        {
            // Arrange
            _repoMock.Setup(x => x.GetAll(It.IsAny<string>()))
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
            _repoMock.Setup(x => x.GetAll(It.IsAny<string>()))
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
            _repoMock.Setup(x => x.GetById(It.IsAny<Guid>()))
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
            _repoMock.Setup(x => x.GetById(testGuid1))
                .ReturnsAsync(TestData.GetUserEvents().ToList()[0]);
            _repoMock.Setup(x => x.GetById(testGuid2))
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
        public async Task Create_ShouldReturnManyEvents_WhenIsRecurring(UserEvent userEvent, int createdEventsCount)
        {
            // Arrange
            _repoMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<UserEvent>>()))
                .Verifiable();

            // Act
            var results = await _sut.AddNewUserEvent(userEvent);

            // Assert
            results.Count().ShouldBe(createdEventsCount);
        }

        //[Fact]        
        //public async Task Create_ShouldThrow_WhenIsRecurringIsInvalid()
        //{
        //    // Arrange
        //    var userEvent = TestData.GetUserEvents().First();
        //    userEvent.RecurrencyRule.Recurrency = (Recurrency)99;
        //    _repoMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<UserEvent>>()))
        //        .Verifiable();

        //    // Act
        //    var func = async () => await _sut.AddNewUserEvent(userEvent);

        //    // Assert
        //    await func.ShouldThrowAsync<ArgumentException>();
        //}

        public static IEnumerable<object[]> ProvideTestData()
        {
            // Provide new Event and count of events, which should be created according to recurrency propery
            var startDateTime = new DateTime(2023, 6, 1, 12, 0, 0);

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
                    
                },
                1
            };
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
                    
                },
                8
            };
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
                    
                },
                6
            };
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
                    
                },
                4
            };
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
                    
                },
                4
            };
        }
    }
}
