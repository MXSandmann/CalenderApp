using Quartz;

namespace ApplicationCore.Jobs
{
    internal class SendEmailJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("--> Sending email");
            return Task.FromResult(true);
        }
    }
}
