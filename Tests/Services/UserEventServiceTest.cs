using Moq;
using Shouldly;
using ApplicationCore.Models;
using ApplicationCore.Repositories.Contracts;
using ApplicationCore.Services;
using ApplicationCore.Services.Contracts;
using Tests;

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
            _repoMock.Setup(x => x.GetAll())
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
            _repoMock.Setup(x => x.GetAll())
                .ReturnsAsync(TestData.GetUserEvents());

            // Act
            var results = await _sut.GetUserEvents();

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
            var result = await _sut.GetUserEventById(Guid.NewGuid());

            // Assert
            result.ShouldBeNull();
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
    }
}
