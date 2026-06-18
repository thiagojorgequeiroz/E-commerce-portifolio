using Catalog.Application.Extensions;
using Catalog.Database.ProgramConfiguration;
using CatalogWebApi.ProgramConfiguration.Exceptions;
using CatalogWebApi.ProgramConfiguration.Swagger.Extensions;
using CatalogWebApi.ProgramConfiguration.Versioning;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApiVersioningConfiguration();

builder.Services.AddApplication();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwaggerDocumentation();

app.UseExceptionHandler();

app.MapControllers();

app.Run();