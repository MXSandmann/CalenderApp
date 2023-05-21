using MassTransit;
using MassTransit.Initializers.PropertyConverters;

namespace WebAPI.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void RegisterMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            var host = configuration.GetValue<string>("MessageBus:Host");
            var username = configuration.GetValue<string>("MessageBus:User");
            var password = configuration.GetValue<string>("MessageBus:Password");
            var port = configuration.GetValue<int>("MessageBus:Port");

            services.AddMassTransit(c =>
            {
                c.SetKebabCaseEndpointNameFormatter();
                c.SetInMemorySagaRepositoryProvider();

                var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(x => x.FullName?.Contains("ApplicationCore") == true);

                ArgumentNullException.ThrowIfNull(assembly);

                c.AddConsumers(assembly);
                c.AddSagaStateMachines(assembly);
                c.AddSagas(assembly);
                c.AddActivities(assembly);
                c.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(host, "/", h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
