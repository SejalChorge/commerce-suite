namespace CommerceSuite.Api.DependencyInjection;

using CommerceSuite.Api.OpenApi;
using CommerceSuite.Api.Serialization;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

public static class ServiceExtensions
{
    /// <summary>
    /// Registers all application services including controllers, minimal APIs, and configuration.
    /// </summary>
    public static void RegisterApplicationServices(this WebApplicationBuilder builder)
    {
        RegisterControllers(builder);
        RegisterMinimalApis(builder);
        RegisterOpenApiServices(builder);
        RegisterSerialization(builder);
    }

    private static void RegisterControllers(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = false;
            });
    }

    private static void RegisterMinimalApis(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
    }

    private static void RegisterOpenApiServices(WebApplicationBuilder builder)
    {
        builder.AddSwaggerConfiguration();
    }

    private static void RegisterSerialization(WebApplicationBuilder builder)
    {
        builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.SerializerOptions.WriteIndented = false;
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });
    }
}
