using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RandomUser.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<RandomUserContext>(options 
                    => options.UseSqlServer(configuration.GetConnectionString("RandomUserDatabase")));

            return services;
        }
    }
}
