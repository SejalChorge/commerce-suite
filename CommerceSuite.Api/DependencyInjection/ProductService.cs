namespace CommerceSuite.Api.DependencyInjection;

using CommerceSuite.Api.Models;

public class ProductService : IProductService
{
    private static readonly List<ProductDto> Products = new()
    {
        new ProductDto(1, "Laptop", 999.99m, "High-performance laptop"),
        new ProductDto(2, "Mouse", 29.99m, "Wireless mouse"),
        new ProductDto(3, "Keyboard", 79.99m, "Mechanical keyboard"),
        new ProductDto(4, "Monitor", 299.99m, "4K monitor"),
        new ProductDto(5, "Headphones", 149.99m, "Noise-cancelling headphones")
    };

    public Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        return Task.FromResult(Products.AsEnumerable());
    }

    public Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }

    public Task<ProductDto> CreateProductAsync(ProductDto product)
    {
        var newId = Products.Max(p => p.Id) + 1;
        var newProduct = new ProductDto(newId, product.Name, product.Price, product.Description);
        Products.Add(newProduct);
        return Task.FromResult(newProduct);
    }
}
