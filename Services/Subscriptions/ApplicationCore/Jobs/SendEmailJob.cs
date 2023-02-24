using ApplicationCore.Services.Contracts;
using Microsoft.Extensions.Logging;
using Quartz;

namespace ApplicationCore.Jobs
{
    public class SendEmailJob : IJob
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<SendEmailJob> _logger;

        public SendEmailJob(IEmailService emailService, ILogger<SendEmailJob> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.JobDetail.JobDataMap;
            var userName = dataMap.GetString("UserName");
            var userEmail = dataMap.GetString("UserEmail");
            var eventName = dataMap.GetString("EventName");
            _logger.LogInformation("--> Sending email to {userName}, {userEmail}", userName, userEmail);
            await _emailService.SendEmail(userName!, userEmail!, eventName!);            
        }
    }
}
