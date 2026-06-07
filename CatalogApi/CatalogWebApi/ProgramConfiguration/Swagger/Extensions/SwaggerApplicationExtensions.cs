using Asp.Versioning.ApiExplorer;

namespace CatalogWebApi.ProgramConfiguration.Swagger.Extensions
{
    public static class SwaggerApplicationExtensions
    {
        public static WebApplication UseSwaggerDocumentation(
            this WebApplication app)
        {
            app.UseSwagger();

            var provider =
                app.Services.GetRequiredService<
                    IApiVersionDescriptionProvider>();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }
    }
}