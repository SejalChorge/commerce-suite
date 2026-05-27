namespace CommerceSuite.Api.Models;

public record OrderDto(int Id, string OrderNumber, decimal TotalAmount, DateTime CreatedAt);
