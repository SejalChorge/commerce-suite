namespace CommerceSuite.Api.Models;

public class OrderDto
{
    public int Id { get; set; } = 100;

    public string Status { get; set; } = "Pending";

    public string Currency { get; set; } = "USD";

    public CustomerDto Customer { get; set; } = new();

    public List<OrderItemDto> Items { get; set; } = new();

    public decimal TotalAmount { get; set; }
}
