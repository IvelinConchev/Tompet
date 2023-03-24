namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.EntityFrameworkCore;
    using Tompet.Core.Contracts;
    using Tompet.Core.Services;
    using Tompet.Core.Services.Statistics;
    using Tompet.Core.Services.Techniques;
    using Tompet.Infrastructure.Data;
    using Tompet.Infrastructure.Repositories;

    public static class ServiceCollectionExtension
    {
        public static IApplicationBuilder PreparateDatabase(this IApplicationBuilder services)
        {
            using var scopedServices = services.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<TompetDbContext>();

            SeedService(data);
            return services;
        }

        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbRepository, ApplicationDbRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileService, FileService>();
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<ITechniqueService, TechniqueService>();

            return services;
        }

        public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<TompetDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

        private static void SeedService(TompetDbContext data)
        {
            if (data.Services.Any())
            {
                return;
            }

            //data.Services.AddRange(new[]
            //{
            //    new Service { Name = "Почистване на канали" },
            //    new Service { Name = "Почистване на ями" },
            //    new Service { Name = "Изграждане на микропричиствателни" },
            //    new Service { Name = "Откриване на течове с камера" },
            //    new Service { Name = "Изкопаване на ями" }
            //});

            //data.SaveChanges();
        }
    }
}
