using Dsw2026Ej15.Data;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Api.Configurations
{
    public static class PersistenceConfigurationExtensions
    {
        public static IServiceCollection AddPersistenceEf(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
