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

        public InvitationService(IInvitationRepository invitationRepository, IPublishEndpoint bus, ILogger<IInvitationService> logger)
        {
            _invitationRepository = invitationRepository;
            _bus = bus;
            _logger = logger;
        }

        public async Task<Invitation> AddInvitation(Invitation invitation, string userName)
        {
            var newInvitationId = await _invitationRepository.Add(invitation);
            var newInvitation = await _invitationRepository.GetById(newInvitationId);

            _logger.LogInformation("--> Publishing created invitation: {value}", JsonConvert.SerializeObject(newInvitation));
            await _bus.Publish(new InvitationCreated(newInvitation.Id, newInvitation.EventId, newInvitation.Email, newInvitation.Role, userName));
            return newInvitation;
        }

        public async Task<IEnumerable<Invitation>> GetAllInvitations()
        {
            return await _invitationRepository.GetAll();
        }
    }
}