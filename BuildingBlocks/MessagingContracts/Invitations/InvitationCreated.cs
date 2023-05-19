namespace MessagingContracts.Invitations
{
    public record InvitationCreated(Guid Id, Guid EventId, string Email, string Role);
}
