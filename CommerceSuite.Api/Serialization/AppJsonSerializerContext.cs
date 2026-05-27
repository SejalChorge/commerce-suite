using System.Text.Json.Serialization;
using CommerceSuite.Api.Models;

namespace CommerceSuite.Api.Serialization;

[JsonSerializable(typeof(OrderDto))]
[JsonSerializable(typeof(ProductDto))]
[JsonSerializable(typeof(ApiResponse<OrderDto>))]
[JsonSerializable(typeof(ApiResponse<ProductDto>))]
[JsonSerializable(typeof(List<OrderDto>))]
[JsonSerializable(typeof(List<ProductDto>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}