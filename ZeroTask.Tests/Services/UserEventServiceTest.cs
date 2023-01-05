using Moq;
using Shouldly;
using ZeroTask.BLL.Services;
using ZeroTask.BLL.Services.Contracts;
using ZeroTask.DAL.Repositories.Contracts;

namespace ZeroTask.Tests.Services
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
    }
}
