using Domain.Models.Account;
using Microsoft.AspNetCore.Identity;
using SQLiteDAL.Repositories;

namespace EffectiveCats.Extensions
{
    public static class InitDatabaseExtension
    {
        public static WebApplication InitDatabase(this WebApplication host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MainContext>();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    DbInitializer.Initialize(context, userManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }

            return host;
        }
    }
}
