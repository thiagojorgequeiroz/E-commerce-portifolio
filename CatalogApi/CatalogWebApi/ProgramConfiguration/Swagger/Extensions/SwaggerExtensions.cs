using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CatalogWebApi.ProgramConfiguration.Swagger.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(
            this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddTransient<
                IConfigureOptions<SwaggerGenOptions>,
                ConfigureSwaggerOptions>();

            services.AddSwaggerGen();

            return services;
        }
    }
}
