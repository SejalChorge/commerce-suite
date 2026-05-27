namespace CommerceSuite.Api.Models;

public record ErrorResponse(
    string Message,
    string? Details = null)
{
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
