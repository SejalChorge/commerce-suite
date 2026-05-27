namespace CommerceSuite.Api.OpenApi;

using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

/// <summary>
/// Configures OpenAPI (Swagger) for the application with support for:
/// - XML documentation comments
/// - MVC controllers
/// - Minimal APIs
/// - Professional UI customization
/// - Production-safe defaults
/// </summary>
public static class SwaggerConfiguration
{
    /// <summary>
    /// Registers OpenAPI and Swagger services with comprehensive configuration.
    /// </summary>
    public static void AddSwaggerConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();

        builder.Services.AddSwaggerGen(options =>
        {
            // Document metadata - displayed in Swagger UI
            options.SwaggerDoc("v1", new()
            {
                Title = "CommerceSuite API",
                Version = "v1",
                Description = "A clean, enterprise-style e-commerce API with comprehensive OpenAPI documentation",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new()
                {
                    Name = "Commerce Team",
                    Email = "support@commerce-suite.com",
                    Url = new Uri("https://github.com/SejalChorge/commerce-suite")
                },
                License = new()
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            // XML documentation integration - includes code comments
            var xmlFile = Path.Combine(AppContext.BaseDirectory, "CommerceSuite.Api.xml");
            if (File.Exists(xmlFile))
            {
                options.IncludeXmlComments(xmlFile);
            }

            // Make API endpoints case-insensitive
            options.IgnoreObsoleteActions();
            options.IgnoreObsoleteProperties();

            // Group endpoints by controller tags
            options.UseInlineDefinitionsForEnums();
        });
    }

    /// <summary>
    /// Configures Swagger UI middleware with professional customization.
    /// Only enables in Development environment for production safety.
    /// </summary>
    public static void UseSwaggerConfigured(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return; // Disable Swagger in Production/Staging
        }

        //// Enable OpenAPI endpoint
        //app.MapOpenApi();

        // Configure Swagger middleware
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "openapi/{documentName}.json";
        });

        // Configure Swagger UI
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "CommerceSuite API v1");

            // Professional UI configuration
            options.RoutePrefix = "api-docs"; // Access at /api-docs
            options.DocumentTitle = "CommerceSuite API Documentation";

            // UI Theme and customization
            options.EnableFilter(); // Show filter input box

            // Display settings
            options.DisplayOperationId();
            options.DefaultModelsExpandDepth(0);
            options.DefaultModelExpandDepth(1);

            // DocExpansion options: "list" (default), "full", "none"
            options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
        });
    }
}
