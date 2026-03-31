namespace TaboAni.Api.Application.DTOs.Response;

public sealed class ErrorResponseDto
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public IReadOnlyCollection<string> Errors { get; init; } = Array.Empty<string>();
}
