using ApplicationCore.Jobs;
using Quartz;

namespace ApplicationCore.Factories
{
    public static class NotificationsFactory
    {

        public static async Task ScheduleEmail(IScheduler scheduler, string userEmail, string userName, string eventName, DateTime notificationTime, Guid notificationId, CancellationToken cancellationToken)
        {

            var job = JobBuilder.Create<SendEmailJob>()
                .UsingJobData("UserName", userName)
                .UsingJobData("UserEmail", userEmail)
                .UsingJobData("EventName", eventName)
                .WithIdentity($"Job_{notificationId}")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"Trigger_{notificationId}")
                .StartAt(notificationTime)
                .WithSimpleSchedule()
                .Build();

            await scheduler.ScheduleJob(job, trigger, cancellationToken);
        }
    }
}
