namespace TaboAni.Api.Application.DTOs.Response;

public sealed record ProduceCategoryResponseDto(
    Guid ProduceCategoryId,
    string CategoryName,
    string? Description);
