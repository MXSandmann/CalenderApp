using Quartz;

namespace ApplicationCore.Jobs
{
    public class SendEmailJobListener : IJobListener
    {
        public string Name => "SendEmailJobListener";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"--> Job vetoed: {context.JobDetail.Key.Name}");
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"--> Job is to be executed: {context.JobDetail.Key.Name}");
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"--> Job was executed: {context.JobDetail.Key.Name}");
            return Task.CompletedTask;
        }
    }
}
