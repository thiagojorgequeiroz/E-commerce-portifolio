using Catalog.Database.Context;
using Catalog.Database.Repositories;
using Catalog.Domain.Respositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Catalog.Database.ProgramConfiguration
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            ConfigureOptions(services, configuration);

            ConfigureDatabase(services);

            ConfigureRepositories(services);

            return services;
        }

        private static void ConfigureOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<DatabaseOptions>()
                .Bind(configuration.GetSection("Database"))
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }

        private static void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<CatalogDbContext>((serviceProvider, options) =>
            {
                var postgresOptions =
                    serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>();

                options.UseNpgsql(
                    postgresOptions.Value.ConnectionString);
            });
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IProductsRespository, ProductsRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
