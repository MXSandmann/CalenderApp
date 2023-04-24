using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions
{
    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder UpdateDatabase(this IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<IHost>>();
            try
            {
                var context = scope.ServiceProvider.GetRequiredService<InvitationDataContext>();
                logger.LogInformation("--> Applying migrations / updating database...");
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                logger.LogError("--> Error applying migrations / updating database: {error}", ex.Message);
            }
            return applicationBuilder;
        }
    }
}
