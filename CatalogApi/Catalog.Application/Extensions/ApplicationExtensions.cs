
using Catalog.Application.Command;
using Catalog.Application.Contract;
using Catalog.Application.Query;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(
                    typeof(ApplicationCommandAssemblyReference).Assembly);
                cfg.RegisterServicesFromAssembly(
                    typeof(ApplicationQueryAssemblyReference).Assembly);
            });

            services.AddValidatorsFromAssemblyContaining<ApplicationContractAssemblyReference>();

            return services;
        }
    }
}
