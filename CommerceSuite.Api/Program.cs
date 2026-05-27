using CommerceSuite.Api.DependencyInjection;
using CommerceSuite.Api.Middleware;
using CommerceSuite.Api.MinimalApis;
using CommerceSuite.Api.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Register all application services and configurations
builder.RegisterApplicationServices();

var app = builder.Build();

// Configure middleware pipeline
app.UseExceptionHandling();
app.UseRequestLogging();
app.UseHttpsRedirection();

// Configure OpenAPI and Swagger documentation (Development only)
app.UseSwaggerConfigured();

// Map controllers and minimal APIs
app.MapControllers();
app.MapHealthAndUtilityEndpoints();

app.Run();
