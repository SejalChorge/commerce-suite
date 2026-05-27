namespace CommerceSuite.Api.Models;

public class ProductDto
{
    public int Id { get; set; } = 1000;

    public string Name { get; set; } = "Sample Product";

    public decimal Price { get; set; } = 99.99m;

    public string Currency { get; set; } = "USD";

    public string Description { get; set; } = "Sample product description";

    public ProductCategoryDto Category { get; set; } = new();

    public ProductInventoryDto Inventory { get; set; } = new();

}
