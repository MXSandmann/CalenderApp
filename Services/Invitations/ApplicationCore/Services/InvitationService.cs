using ApplicationCore.Models.Entities;
using ApplicationCore.Repositories;
using ApplicationCore.Services.Contracts;
using MassTransit;
using MessagingContracts.Invitations;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApplicationCore.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IPublishEndpoint _bus;
        private readonly ILogger<IInvitationService> _logger;
        private readonly IEmailService _emailService;

        public InvitationService(IInvitationRepository invitationRepository, IPublishEndpoint bus, ILogger<IInvitationService> logger, IEmailService emailService)
        {
            _invitationRepository = invitationRepository;
            _bus = bus;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<Invitation> AddInvitation(Invitation invitation, string userName)
        {
            var newInvitationId = await _invitationRepository.Add(invitation);
            var newInvitation = await _invitationRepository.GetById(newInvitationId);

            _logger.LogInformation("--> Publishing created invitation: {value}", JsonConvert.SerializeObject(newInvitation));

            // Send an email to invited user
            await _emailService.SendEmail(invitation.Email);

            // If an invitation has created to an event, send a message to message broker
            if(newInvitation.EventId is not null
                || newInvitation.EventId != Guid.Empty)
                await _bus.Publish(new InvitationCreated(newInvitation.Id, newInvitation.EventId!.Value, newInvitation.Email, newInvitation.Role, userName));

            return newInvitation;
        }

        public async Task<IEnumerable<Invitation>> GetAllInvitations()
        {
            return await _invitationRepository.GetAll();
        }
    }
}