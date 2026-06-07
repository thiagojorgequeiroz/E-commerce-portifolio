using Catalog.Application.Extensions;
using CatalogWebApi.ProgramConfiguration.Swagger.Extensions;
using CatalogWebApi.ProgramConfiguration.Versioning;
using Catalog.Database.ProgramConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApiVersioningConfiguration();

builder.Services.AddApplication();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddDatabase(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();