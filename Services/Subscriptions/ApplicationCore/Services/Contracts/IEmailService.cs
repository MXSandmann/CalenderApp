namespace ApplicationCore.Services.Contracts
{
    public interface IEmailService
    {
        Task SendEmail(string userName, string userEmail, string eventName);
    }
}
