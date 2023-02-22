using ApplicationCore.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

namespace ApplicationCore.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddQuartzScheduler(this IServiceCollection services)
        {
            var quartz = CofigureQuartz();
            services.AddSingleton(p => quartz);
            return services;
        }

        private static IScheduler CofigureQuartz()
        {
            var props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            var factory = new StdSchedulerFactory(props);
            var scheduler = factory.GetScheduler().Result;
            scheduler.Start().Wait();
            scheduler.ListenerManager.AddTriggerListener(new SendEmailTriggerListener());
            scheduler.ListenerManager.AddJobListener(new SendEmailJobListener());
            scheduler.ListenerManager.AddSchedulerListener(new SendEmailSchedulerListener());
            return scheduler;
        }
    }
}
