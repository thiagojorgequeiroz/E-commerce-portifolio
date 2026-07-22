using CatalogWebApi.Grpc.Inventory;

namespace CatalogWebApi.ProgramConfiguration.Grpc
{
    public static class GrpcExtensions
    {
        public static IServiceCollection AddGrpcConfiguration(
            this IServiceCollection services)
        {
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
            });

            services.AddGrpcReflection();

            return services;
        }

        public static WebApplication UseGrpc(
            this WebApplication app)
        {
            app.MapGrpcService<InventoryGrpcService>();

            if (app.Environment.IsDevelopment())
            {
                app.MapGrpcReflectionService();
            }

            return app;
        }
    }
}
