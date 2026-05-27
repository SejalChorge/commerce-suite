using CommerceSuite.ApiClient.Generated;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("http://localhost:5217")
};

var client = new CommerceSuiteClient(httpClient);

try
{
    var response = await client.GetProductsAsync();

    var products = response.Data;

    Console.WriteLine($"Products Count: {products.Count}");

    foreach (var product in products)
    {
        Console.WriteLine($"{product.Name} - {product.Price}");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}