using Quartz;

namespace ApplicationCore.Jobs.Listeners
{
    public class SendEmailSchedulerListener : ISchedulerListener
    {
        public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"--> Job added: {jobDetail.Key.Name}");
            return Task.CompletedTask;
        }

        public Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"--> Job deleted: {jobKey.Name}");
            return Task.CompletedTask;
        }

        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"--> Job interrupted: {jobKey.Name}");
            return Task.CompletedTask;
        }

        public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"--> Job paused: {jobKey.Name}");
            return Task.CompletedTask;
        }

        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"--> Job resumed: {jobKey.Name}");
            return Task.CompletedTask;
        }

        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"--> Job scheduled: {trigger.Key.Name} at {trigger.GetFireTimeAfter(DateTime.UtcNow)}");
            return Task.CompletedTask;
        }

        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"--> Job unscheduled: {triggerKey.Name}");
            return Task.CompletedTask;
        }

        public Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"--> Scheduler Error: {msg}");
            return Task.CompletedTask;
        }

        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"--> Scheduler In StandbyMode");
            return Task.CompletedTask;
        }

        public Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"--> Scheduler Shutdown");
            return Task.CompletedTask;
        }

        public Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            Console.WriteLine("--> " + nameof(SchedulerShuttingdown));
            return Task.CompletedTask;
        }

        public Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            Console.WriteLine("--> " + nameof(SchedulerStarted));
            return Task.CompletedTask;
        }

        public Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            Console.WriteLine("--> " + nameof(SchedulerStarting));
            return Task.CompletedTask;
        }

        public Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            Console.WriteLine("--> " + nameof(SchedulingDataCleared));
            return Task.CompletedTask;
        }

        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task TriggersPaused(string? triggerGroup, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task TriggersResumed(string? triggerGroup, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
