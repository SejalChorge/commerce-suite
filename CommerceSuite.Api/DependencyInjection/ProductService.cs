namespace CommerceSuite.Api.DependencyInjection;

using CommerceSuite.Api.Models;

public class ProductService : IProductService
{
    private static readonly List<ProductDto> Products = new()
    {
        new ProductDto
        {
            Id = 1,Name = "Laptop", Description = "High-performance laptop", Price = 1000
        },
        new ProductDto
        {
            Id = 1,Name = "Mouse", Description = "Wireless mouse", Price = 200
        },
        new ProductDto
        {
            Id = 1,Name = "Keyboard", Description = "Mechanical keyboard", Price = 500
        },
        new ProductDto
        {
            Id = 1,Name = "Monitor", Description = "4K monitor", Price = 1000
        },
        new ProductDto
        {
            Id = 1,Name = "Headphones", Description = "Noise cancelling headphones", Price = 500
        }
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
        var newProduct = new ProductDto() { Category = product.Category, Description = product.Description, Id = newId, Name = product.Name, Price = product.Price };
        Products.Add(newProduct);
        return Task.FromResult(newProduct);
    }
}
