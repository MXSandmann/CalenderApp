using ApplicationCore.Models.Dtos;
using ApplicationCore.Models.Entities;
using ApplicationCore.Profiles;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using WebAPI.Controllers;

namespace Tests.Controllers
{
    public class SubscriptionsControllerTest
    {
        private readonly Mock<ILogger<SubscriptionsController>> _mockLogger;
        private readonly Mock<ISubscriptionService> _mockService;
        private readonly IMapper _mapper;
        private readonly SubscriptionsController _sut;

        public SubscriptionsControllerTest()
        {
            _mockLogger = new Mock<ILogger<SubscriptionsController>>();
            _mockService = new Mock<ISubscriptionService>();
            var profile = new AutomapperProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(config);
            _sut = new SubscriptionsController(_mockLogger.Object, _mockService.Object, _mapper);
        }

        [Fact]
        public async Task Get_ShouldReturnSubscriptions_WhenAllOK()
        {
            // Arrange
            var subscrs = TestData.GetSubscriptions();
            var subscrDtos = _mapper.Map<IEnumerable<SubscriptionDto>>(subscrs);
            _mockService.Setup(x => x.GetSubscriptions()).ReturnsAsync(subscrs);

            // Act
            var results = await _sut.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(results);
            var returnedDtos = Assert.IsType<List<SubscriptionDto>>(okResult.Value);
            Assert.Equal(JsonConvert.SerializeObject(subscrDtos), JsonConvert.SerializeObject(returnedDtos));
        }

        [Fact]
        public async Task GetById_ShouldReturnSubscription_WhenAllOK()
        {
            // Arrange
            var subscr = TestData.GetSubscriptions().First();
            var subscrDto = _mapper.Map<SubscriptionDto>(subscr);
            _mockService.Setup(x => x.GetSubscriptionById(It.IsAny<Guid>())).ReturnsAsync(subscr);

            // Act
            var result = await _sut.GetById(Guid.NewGuid());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<SubscriptionDto>(okResult.Value);
            Assert.Equal(JsonConvert.SerializeObject(subscrDto), JsonConvert.SerializeObject(returnedDto));
        }

        [Fact]
        public async Task AddSubscription_ShouldReturnSubscription_WhenAllOK()
        {
            // Arrange
            var subscr = TestData.GetSubscriptions().First();
            var subscrDto = _mapper.Map<SubscriptionDto>(subscr);
            _mockService.Setup(x => x.CreateSubscription(It.IsAny<Subscription>())).ReturnsAsync(subscr);

            // Act
            var result = await _sut.AddSubscription(subscrDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<SubscriptionDto>(okResult.Value);
            Assert.Equal(JsonConvert.SerializeObject(subscrDto), JsonConvert.SerializeObject(returnedDto));
        }

        [Fact]
        public async Task Update_ShouldReturnSubscription_WhenAllOK()
        {
            // Arrange
            var subscr = TestData.GetSubscriptions().First();
            var subscrDto = _mapper.Map<SubscriptionDto>(subscr);
            _mockService.Setup(x => x.UpdateSubscription(It.IsAny<Subscription>())).ReturnsAsync(subscr);

            // Act
            var result = await _sut.Update(subscrDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<SubscriptionDto>(okResult.Value);
            Assert.Equal(JsonConvert.SerializeObject(subscrDto), JsonConvert.SerializeObject(returnedDto));
        }

    }
}
