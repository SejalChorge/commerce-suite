namespace CommerceSuite.Api.DependencyInjection;

using CommerceSuite.Api.Models;

public class OrderService : IOrderService
{
    private static readonly List<OrderDto> Orders = new()
    {
        new OrderDto(1, "ORD-001", 1500.00m, DateTime.UtcNow.AddDays(-5)),
        new OrderDto(2, "ORD-002", 2300.50m, DateTime.UtcNow.AddDays(-3)),
        new OrderDto(3, "ORD-003", 999.99m, DateTime.UtcNow.AddDays(-1))
    };

    public Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        return Task.FromResult(Orders.AsEnumerable());
    }

    public Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        var order = Orders.FirstOrDefault(o => o.Id == id);
        return Task.FromResult(order);
    }
}
