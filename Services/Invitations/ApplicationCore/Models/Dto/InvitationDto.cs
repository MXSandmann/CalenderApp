namespace ApplicationCore.Models.Dto
{
    public class InvitationDto
    {
        /// <summary>
        /// Name of a user, who has created an invitation
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        public Guid EventId { get; set; }
        /// <summary>
        /// Email of the invited person
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// The role of the invited person
        /// </summary>
        public string Role { get; set; } = string.Empty;
    }
}
