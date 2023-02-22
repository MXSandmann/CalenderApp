using Quartz;

namespace ApplicationCore.Jobs
{
    internal class SendEmailJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.JobDetail.JobDataMap;
            var userName = dataMap.GetString("UserName");
            var userEmail = dataMap.GetString("UserEmail");
            Console.WriteLine($"--> Sending email to {userName}, {userEmail}");
            return Task.FromResult(true);
        }
    }
}
