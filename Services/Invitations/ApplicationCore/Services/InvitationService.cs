using ApplicationCore.Models.Entities;
using ApplicationCore.Repositories;
using ApplicationCore.Services.Contracts;
using MassTransit;
using MessagingContracts.Invitations;

namespace ApplicationCore.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IPublishEndpoint _bus;

        public InvitationService(IInvitationRepository invitationRepository, IPublishEndpoint bus)
        {
            _invitationRepository = invitationRepository;
            _bus = bus;
        }

        public async Task<Invitation> AddInvitation(Invitation invitation)
        {
            var newInvitationId = await _invitationRepository.Add(invitation);
            var newInvitation = await _invitationRepository.GetById(newInvitationId);
            await _bus.Publish(new InvitationCreated(newInvitation.Id, newInvitation.EventId, newInvitation.Email, newInvitation.Role));
            return newInvitation;
        }
    }
}