namespace CommerceSuite.Api.DependencyInjection;

using CommerceSuite.Api.Models;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductDto> CreateProductAsync(ProductDto product);
}
