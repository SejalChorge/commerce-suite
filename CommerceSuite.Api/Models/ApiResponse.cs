namespace CommerceSuite.Api.Models;

public record ApiResponse<T>(
    bool Success,
    T? Data = default,
    string Message = "Request processed successfully")
{
    public static ApiResponse<T> Ok(T data, string? message = null) =>
        new(
            Success: true,
            Data: data,
            Message: message ?? "Request processed successfully");

    public static ApiResponse<T> Fail(string message) =>
        new(
            Success: false,
            Message: message);
}