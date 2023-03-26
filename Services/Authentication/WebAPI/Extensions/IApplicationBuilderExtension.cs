using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions
{
    public static class IApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> UpdateDatabase(this IApplicationBuilder applicationBuilder, IConfiguration configuration)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<IHost>>();
            try
            {
                var context = scope.ServiceProvider.GetRequiredService<UserDataContext>();
                logger.LogInformation("--> Applying migrations / updating database...");
                context.Database.Migrate();

                var password = configuration.GetValue<string>("Users:Admin:Password");
                ArgumentException.ThrowIfNullOrEmpty(password);

                await SeedAdmin(context, password);
            }
            catch (Exception ex)
            {
                logger.LogError("--> Error applying migrations / updating database: {error}", ex.Message);
            }
            return applicationBuilder;
        }

        private static async Task SeedAdmin(UserDataContext context, string password)
        {
            if (context.Users.Any(x => x.Role == Role.Admin))
                return;
            await context.AddAsync(new User
            {
                UserName = "admin",
                Password = password,
                Email = "admin@email.de",
                Role = Role.Admin
            });
            await context.SaveChangesAsync();
        }
    }
}
