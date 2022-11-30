namespace Tompet.Api.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Tompet.Infrastructure.Data;

    public static class ApiServiceCollectionExtension
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddApiDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<TompetDbContext>(options =>
                options.UseSqlServer(connectionString));
            

            return services;
        }

    }
}
