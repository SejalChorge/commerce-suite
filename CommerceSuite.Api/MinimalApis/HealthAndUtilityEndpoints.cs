namespace CommerceSuite.Api.MinimalApis;

using CommerceSuite.Api.DependencyInjection;
using CommerceSuite.Api.Models;

/// <summary>
/// Minimal API endpoints demonstrating lightweight endpoint handlers.
/// These endpoints provide health checks, API information, and product retrieval
/// using the minimal APIs pattern introduced in .NET 6+.
/// </summary>
public static class HealthAndUtilityEndpoints
{
    /// <summary>
    /// Maps sample minimal API endpoints to the application.
    /// </summary>
    /// <param name="app">The web application to map endpoints to.</param>
    /// <remarks>
    /// This method registers three endpoints:
    /// - Health check for monitoring
    /// - API information and metadata
    /// - Alternative product listing endpoint using minimal API pattern
    /// 
    /// All endpoints include OpenAPI documentation via WithOpenApi().
    /// </remarks>
    public static void MapHealthAndUtilityEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/samples")
            .WithOpenApi()
            .WithName("Samples")
            .WithDescription("Sample minimal API endpoints demonstrating lightweight endpoint handlers");

        group.MapGet("/health", GetHealth)
            .WithName("Health Check")
            .WithDescription("Returns the current health status of the API")
            .Produces<ApiResponse<HealthResponse>>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
            .WithOpenApi();

        group.MapGet("/info", GetInfo)
            .WithName("API Information")
            .WithDescription("Returns metadata and version information about the CommerceSuite API")
            .Produces<ApiResponse<ApiInfoResponse>>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
            .WithOpenApi();

        group.MapGet("/products-minimal", GetProducts)
            .WithName("Get Products (Minimal API)")
            .WithDescription("Returns all products using the minimal API pattern")
            .Produces<ApiResponse<IEnumerable<ProductDto>>>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
            .WithOpenApi();
    }

    /// <summary>
    /// Health check endpoint to verify API is running.
    /// </summary>
    /// <remarks>
    /// Returns the current health status of the API along with the timestamp.
    /// This endpoint is typically used by load balancers and monitoring systems
    /// to ensure the API is operational.
    /// 
    /// Example response (200 OK):
    /// ```json
    /// {
    ///   "success": true,
    ///   "data": {
    ///     "status": "Healthy",
    ///     "timestamp": "2024-01-15T10:30:45.1234567Z"
    ///   },
    ///   "message": null
    /// }
    /// ```
    /// </remarks>
    /// <returns>
    /// A successful response containing the health status.
    /// Returns HTTP 200 (OK) with a HealthResponse object.
    /// </returns>
    private static IResult GetHealth()
    {
        var response = ApiResponse<HealthResponse>.Ok(
            new HealthResponse(Status: "Healthy", Timestamp: DateTime.UtcNow));
        return Results.Ok(response);
    }

    /// <summary>
    /// API information endpoint providing metadata about the service.
    /// </summary>
    /// <remarks>
    /// Returns comprehensive information about the CommerceSuite API including
    /// version number, service name, and description. This endpoint is useful
    /// for client applications to verify they're connecting to the correct service.
    /// 
    /// Example response (200 OK):
    /// ```json
    /// {
    ///   "success": true,
    ///   "data": {
    ///     "version": "1.0.0",
    ///     "name": "CommerceSuite API",
    ///     "description": "Enterprise-style e-commerce API"
    ///   },
    ///   "message": null
    /// }
    /// ```
    /// </remarks>
    /// <returns>
    /// A successful response containing API metadata.
    /// Returns HTTP 200 (OK) with an ApiInfoResponse object.
    /// </returns>
    private static IResult GetInfo()
    {
        var response = ApiResponse<ApiInfoResponse>.Ok(
            new ApiInfoResponse(
                Version: "1.0.0",
                Name: "CommerceSuite API",
                Description: "Enterprise-style e-commerce API with comprehensive OpenAPI documentation"));
        return Results.Ok(response);
    }

    /// <summary>
    /// Retrieves all products using the minimal API pattern.
    /// </summary>
    /// <remarks>
    /// This endpoint is an alternative to the controller-based GetAll endpoint,
    /// demonstrating the minimal APIs pattern. It provides the same functionality
    /// with reduced boilerplate code.
    /// 
    /// Example response (200 OK):
    /// ```json
    /// {
    ///   "success": true,
    ///   "data": [
    ///     {
    ///       "id": 1,
    ///       "name": "Laptop",
    ///       "price": 999.99,
    ///       "description": "High-performance laptop"
    ///     }
    ///   ],
    ///   "message": null
    /// }
    /// ```
    /// </remarks>
    /// <param name="productService">The product service injected via dependency injection.</param>
    /// <returns>
    /// A successful response containing the list of all products.
    /// Returns HTTP 200 (OK) with a list of ProductDto objects.
    /// </returns>
    private static async Task<IResult> GetProducts(IProductService productService)
    {
        try
        {
            var products = await productService.GetAllProductsAsync();
            var response = ApiResponse<IEnumerable<ProductDto>>.Ok(products);
            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ErrorResponse($"An error occurred: {ex.Message}");
            return Results.InternalServerError();
        }
    }
}

/// <summary>
/// Represents the health status of the API.
/// </summary>
/// <param name="Status">The current health status of the API (e.g., "Healthy").</param>
/// <param name="Timestamp">The UTC timestamp when the health check was performed.</param>
public record HealthResponse(string Status, DateTime Timestamp);

/// <summary>
/// Represents metadata about the CommerceSuite API.
/// </summary>
/// <param name="Version">The API version number (e.g., "1.0.0").</param>
/// <param name="Name">The name of the API service.</param>
/// <param name="Description">A brief description of the API's purpose and functionality.</param>
public record ApiInfoResponse(string Version, string Name, string Description);
