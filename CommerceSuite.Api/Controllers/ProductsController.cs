namespace CommerceSuite.Api.Controllers;

using CommerceSuite.Api.DependencyInjection;
using CommerceSuite.Api.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// API endpoints for managing products in the e-commerce system.
/// Provides CRUD operations with proper HTTP semantics and error handling.
/// </summary>
/// <remarks>
/// This controller demonstrates enterprise-level API design with:
/// - Proper HTTP status codes
/// - Consistent response formatting
/// - Comprehensive error handling
/// - XML documentation for Swagger
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    /// <summary>
    /// Initializes a new instance of the ProductsController.
    /// </summary>
    /// <param name="productService">The product service for business logic.</param>
    /// <param name="logger">The logger for recording controller operations.</param>
    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all products from the system.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a paginated list of all available products.
    /// Products include information such as ID, name, price, and description.
    /// 
    /// Example request:
    /// ```
    /// GET /api/products
    /// ```
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
    /// <returns>
    /// A response containing a list of all products wrapped in an ApiResponse object.
    /// Each product includes its ID, name, price, and description.
    /// </returns>
    /// <response code="200">Successfully retrieved all products.</response>
    /// <response code="500">An internal server error occurred while fetching products.</response>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProductDto>>>> GetAll()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(ApiResponse<IEnumerable<ProductDto>>.Ok(products));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all products");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a specific product by its ID.
    /// </summary>
    /// <remarks>
    /// Returns detailed information about a single product if it exists.
    /// Returns a 404 Not Found response if the product does not exist.
    /// 
    /// Example request:
    /// ```
    /// GET /api/products/1
    /// ```
    /// 
    /// Example response (200 OK):
    /// ```json
    /// {
    ///   "success": true,
    ///   "data": {
    ///     "id": 1,
    ///     "name": "Laptop",
    ///     "price": 999.99,
    ///     "description": "High-performance laptop"
    ///   },
    ///   "message": null
    /// }
    /// ```
    /// </remarks>
    /// <param name="id">The unique identifier of the product to retrieve. Must be a positive integer.</param>
    /// <returns>
    /// A response containing the requested product wrapped in an ApiResponse object if found.
    /// Returns an error response with HTTP 404 if the product does not exist.
    /// </returns>
    /// <response code="200">Product found and returned successfully.</response>
    /// <response code="400">The provided product ID is invalid.</response>
    /// <response code="404">The requested product was not found.</response>
    /// <response code="500">An internal server error occurred while fetching the product.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<ProductDto>>> GetById(int id)
    {
        if (id <= 0)
        {
            _logger.LogWarning("Invalid product ID requested: {ProductId}", id);
            return BadRequest(new ErrorResponse("Invalid product ID. Must be a positive integer."));
        }

        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found", id);
                return NotFound(new ErrorResponse($"Product with ID {id} not found."));
            }

            return Ok(ApiResponse<ProductDto>.Ok(product));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving product with ID {ProductId}", id);
            throw;
        }
    }

    /// <summary>
    /// Creates a new product in the system.
    /// </summary>
    /// <remarks>
    /// Adds a new product to the catalog with the provided details.
    /// The product ID is auto-generated and returned in the response.
    /// 
    /// Validation rules:
    /// - Product name must not be empty or whitespace
    /// - Product price must be greater than zero
    /// - Description is optional
    /// 
    /// Example request:
    /// ```
    /// POST /api/products
    /// Content-Type: application/json
    /// 
    /// {
    ///   "name": "USB-C Cable",
    ///   "price": 19.99,
    ///   "description": "High-speed USB-C charging cable"
    /// }
    /// ```
    /// 
    /// Example response (201 Created):
    /// ```json
    /// {
    ///   "success": true,
    ///   "data": {
    ///     "id": 6,
    ///     "name": "USB-C Cable",
    ///     "price": 19.99,
    ///     "description": "High-speed USB-C charging cable"
    ///   },
    ///   "message": null
    /// }
    /// ```
    /// </remarks>
    /// <param name="product">
    /// The product data to create. Must include name and price.
    /// The ID field should be omitted as it will be generated.
    /// </param>
    /// <returns>
    /// A 201 Created response containing the newly created product with its assigned ID.
    /// The response includes a Location header pointing to the new resource.
    /// </returns>
    /// <response code="201">Product created successfully. Location header contains the URL of the new resource.</response>
    /// <response code="400">The provided product data is invalid or incomplete.</response>
    /// <response code="500">An internal server error occurred while creating the product.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<ProductDto>>> Create([FromBody] ProductDto product)
    {
        if (product == null)
        {
            return BadRequest(new ErrorResponse("Product data is required."));
        }

        if (string.IsNullOrWhiteSpace(product.Name) || product.Price <= 0)
        {
            _logger.LogWarning("Invalid product data received: Name={Name}, Price={Price}", product.Name, product.Price);
            return BadRequest(new ErrorResponse(
                "Invalid product data. Product name must not be empty and price must be greater than zero."));
        }

        try
        {
            var createdProduct = await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, 
                ApiResponse<ProductDto>.Ok(createdProduct, "Product created successfully."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating product with name {ProductName}", product.Name);
            throw;
        }
    }
}
