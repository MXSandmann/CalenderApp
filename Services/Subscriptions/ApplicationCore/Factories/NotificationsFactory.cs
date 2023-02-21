using ApplicationCore.Jobs;
using Quartz;

namespace ApplicationCore.Factories
{
    public static class NotificationsFactory
    {       

        public static async Task ScheduleEmail(IScheduler scheduler, string userEmail, string userName, DateTime notificationTime, CancellationToken cancellationToken)
        {            

            var job = JobBuilder.Create<SendEmailJob>()
                .UsingJobData(userEmail, userName)
                .WithIdentity("testjob")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("testtrigger")
                .StartAt(notificationTime)
                .WithSimpleSchedule()
                .Build();
            
            await scheduler.ScheduleJob(job, trigger, cancellationToken);
        }        
    }
}
