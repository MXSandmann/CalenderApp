using ApplicationCore.Models.Entities;
using ApplicationCore.Repositories;
using ApplicationCore.Services;
using ApplicationCore.Services.Contracts;
using MassTransit;
using MessagingContracts.Invitations;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Controllers
{
    public class InvitationsServiceTest
    {
        private readonly Mock<IInvitationRepository> _mockInvitationRepository;
        private readonly Mock<IPublishEndpoint> _mockBus;
        private readonly Mock<ILogger<IInvitationService>> _mockLogger;
        private readonly IInvitationService _sut;

        public InvitationsServiceTest()
        {
            _mockInvitationRepository = new();
            _mockBus = new();
            _mockLogger = new();
            _sut = new InvitationService(_mockInvitationRepository.Object, _mockBus.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task AddInvitation_ShouldReturnDto_WhenAllOk()
        {
            // Arrange
            var userName = "testname";
            var newInvitationId = Guid.NewGuid();
            var eventId = Guid.NewGuid();
            var newInvitation = GetTestInvitation(newInvitationId, eventId);
            var message = GetTestMessage(newInvitation, userName);
            _mockInvitationRepository.Setup(x => x.Add(It.IsAny<Invitation>()))
                .ReturnsAsync(newInvitationId);
            _mockInvitationRepository.Setup(x => x.GetById(It.Is<Guid>(x => x == newInvitationId)))
                .ReturnsAsync(newInvitation);
            _mockBus.Setup(x => x.Publish(It.Is<InvitationCreated>(x => x == message), CancellationToken.None))
                .Verifiable();

            // Act
            var result = await _sut.AddInvitation(newInvitation, userName);

            // Assert
            Assert.IsType<Invitation>(result);
            Assert.Equal(newInvitationId, result.Id);
            Assert.Equal(eventId, result.EventId);
            _mockBus.Verify(x => x.Publish(It.IsAny<InvitationCreated>(), CancellationToken.None), Times.Once());
            _mockBus.VerifyNoOtherCalls();
        }

        private static Invitation GetTestInvitation(Guid newInvitationId, Guid eventId) =>
            new()
            {
                Id = newInvitationId,
                EventId = eventId,
                Email = "mxxax@protonmail.com",
                Role = "Boss"
            };

        private static InvitationCreated GetTestMessage(Invitation invitation, string userName)
        {
            return new(invitation.EventId, invitation.EventId, invitation.Email, invitation.Role, userName);
        }



    }
}
