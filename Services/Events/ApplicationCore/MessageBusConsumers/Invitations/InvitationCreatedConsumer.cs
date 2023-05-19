using MassTransit;
using MessagingContracts.Invitations;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApplicationCore.MessageBusConsumers.Invitations
{
    public class InvitationCreatedConsumer : IConsumer<InvitationCreated>
    {
        private readonly ILogger<InvitationCreatedConsumer> _logger;

        public InvitationCreatedConsumer(ILogger<InvitationCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<InvitationCreated> context)
        {
            _logger.LogInformation("Received message: {value}", JsonConvert.SerializeObject(context));
            return Task.CompletedTask;
        }
    }
}
