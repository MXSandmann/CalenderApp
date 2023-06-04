namespace MessagingContracts.Invitations
{
    public record InvitationCreated(Guid InvitationId, Guid EventId, string Email, string Role);
}
