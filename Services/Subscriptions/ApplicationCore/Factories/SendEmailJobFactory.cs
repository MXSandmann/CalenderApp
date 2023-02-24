using Quartz;
using Quartz.Simpl;
using Quartz.Spi;

namespace ApplicationCore.Factories
{
    public class SendEmailJobFactory : SimpleJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SendEmailJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                // this will inject dependencies that the job requires
                var newJob = (IJob)_serviceProvider.GetService(bundle.JobDetail.JobType)!;
                return newJob;
            }
            catch (Exception ex)
            {
                throw new SchedulerException(ex.Message);
            }
        }
    }
}
