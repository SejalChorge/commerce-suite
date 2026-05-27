namespace CommerceSuite.Api.DependencyInjection;

using CommerceSuite.Api.Models;

public class OrderService : IOrderService
{
    private static readonly List<OrderDto> Orders = new()
    {
        new OrderDto
        {
            Id = 1,Status = "Pending",Currency = "USD"
        },
        new OrderDto
        {
            Id = 2,Status = "Pending",Currency = "USD"
        },
        new OrderDto{
            Id = 3,Status = "Pending",Currency = "INR"
        }
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
