using Dsw2026Ej15.Data;
using Dsw2026Ej15.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Api.Configuration
{
    public static class PersistenceConfigurationExtensions
    {
        public static IServiceCollection AddApplicationPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString 
                = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<Dsw2026Ej15DbContext>(options => 
            {
                options.UseSqlServer(connectionString);
            });
            return services;
        }

        public static IHost LoadSpecialityData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider
                .GetRequiredService<Dsw2026Ej15DbContext>();
            context.SeedworkSpecialities(@"specialities.json");
            return host;
        }
    }
}
