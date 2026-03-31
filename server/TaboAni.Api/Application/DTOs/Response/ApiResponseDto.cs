namespace TaboAni.Api.Application.DTOs.Response;

public sealed class ApiResponseDto<T>
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public T? Data { get; init; }
}
