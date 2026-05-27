namespace CommerceSuite.Api.Controllers;

using CommerceSuite.Api.DependencyInjection;
using CommerceSuite.Api.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// API endpoints for managing orders in the e-commerce system.
/// Provides operations for retrieving and managing order information.
/// </summary>
/// <remarks>
/// This controller demonstrates enterprise-level API design with:
/// - Proper HTTP status codes and semantics
/// - Comprehensive error documentation
/// - XML comments for Swagger integration
/// - Consistent response formatting
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;

    /// <summary>
    /// Initializes a new instance of the OrdersController.
    /// </summary>
    /// <param name="orderService">The order service for business logic operations.</param>
    /// <param name="logger">The logger for recording controller activities.</param>
    public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all orders from the system.
    /// </summary>
    /// <remarks>
    /// Returns a complete list of all orders in the system.
    /// Each order includes its ID, order number, total amount, and creation date.
    /// Orders are not filtered or paginated in this implementation.
    /// 
    /// Example request:
    /// ```
    /// GET /api/orders
    /// ```
    /// 
    /// Example response (200 OK):
    /// ```json
    /// {
    ///   "success": true,
    ///   "data": [
    ///     {
    ///       "id": 1,
    ///       "orderNumber": "ORD-001",
    ///       "totalAmount": 1500.00,
    ///       "createdAt": "2024-01-10T15:30:00Z"
    ///     }
    ///   ],
    ///   "message": null
    /// }
    /// ```
    /// </remarks>
    /// <returns>
    /// A response containing a list of all orders wrapped in an ApiResponse object.
    /// Each order includes its complete information.
    /// </returns>
    /// <response code="200">Successfully retrieved all orders.</response>
    /// <response code="500">An internal server error occurred while fetching orders.</response>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<OrderDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<IEnumerable<OrderDto>>>> GetAll()
    {
        try
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(ApiResponse<IEnumerable<OrderDto>>.Ok(orders));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all orders");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a specific order by its ID.
    /// </summary>
    /// <remarks>
    /// Returns detailed information about a single order if it exists.
    /// Returns a 404 Not Found response if the order does not exist in the system.
    /// 
    /// Example request:
    /// ```
    /// GET /api/orders/1
    /// ```
    /// 
    /// Example response (200 OK):
    /// ```json
    /// {
    ///   "success": true,
    ///   "data": {
    ///     "id": 1,
    ///     "orderNumber": "ORD-001",
    ///     "totalAmount": 1500.00,
    ///     "createdAt": "2024-01-10T15:30:00Z"
    ///   },
    ///   "message": null
    /// }
    /// ```
    /// </remarks>
    /// <param name="id">
    /// The unique identifier of the order to retrieve.
    /// Must be a positive integer greater than zero.
    /// </param>
    /// <returns>
    /// A response containing the requested order wrapped in an ApiResponse object if found.
    /// Returns an error response with HTTP 404 if the order does not exist.
    /// </returns>
    /// <response code="200">Order found and returned successfully.</response>
    /// <response code="400">The provided order ID is invalid or malformed.</response>
    /// <response code="404">The requested order was not found in the system.</response>
    /// <response code="500">An internal server error occurred while fetching the order.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<OrderDto>>> GetById(int id)
    {
        if (id <= 0)
        {
            _logger.LogWarning("Invalid order ID requested: {OrderId}", id);
            return BadRequest(new ErrorResponse("Invalid order ID. Must be a positive integer."));
        }

        try
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                _logger.LogWarning("Order with ID {OrderId} not found", id);
                return NotFound(new ErrorResponse($"Order with ID {id} not found."));
            }

            return Ok(ApiResponse<OrderDto>.Ok(order));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving order with ID {OrderId}", id);
            throw;
        }
    }
}
