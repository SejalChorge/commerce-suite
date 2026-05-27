namespace CommerceSuite.Api.Models;

public record ApiResponse<T>(
    bool Success,
    T? Data = default,
    string? Message = null)
{
    public static ApiResponse<T> Ok(T data, string? message = null) =>
        new(Success: true, Data: data, Message: message);

    public static ApiResponse<T> Fail(string message) =>
        new(Success: false, Message: message);
}
