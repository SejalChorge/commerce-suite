namespace CommerceSuite.Api.DependencyInjection;

using CommerceSuite.Api.Models;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto?> GetOrderByIdAsync(int id);
}
