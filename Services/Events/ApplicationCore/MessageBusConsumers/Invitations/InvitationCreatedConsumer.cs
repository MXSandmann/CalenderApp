using ApplicationCore.Services.Contracts;
using MassTransit;
using MessagingContracts.Invitations;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApplicationCore.MessageBusConsumers.Invitations
{
    public class InvitationCreatedConsumer : IConsumer<InvitationCreated>
    {
        private readonly ILogger<InvitationCreatedConsumer> _logger;
        private readonly IUserEventService _userEventService;

        public InvitationCreatedConsumer(ILogger<InvitationCreatedConsumer> logger, IUserEventService userEventService)
        {
            _logger = logger;
            _userEventService = userEventService;
        }

        public async Task Consume(ConsumeContext<InvitationCreated> context)
        {
            _logger.LogInformation("--> Received message from service bus: {value}", JsonConvert.SerializeObject(context.Message));
            await _userEventService.AddInvitationToUserEvent(context.Message.EventId, context.Message.InvitationId, context.Message.UserName);
        }
    }
}
