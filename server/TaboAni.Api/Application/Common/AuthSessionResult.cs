using TaboAni.Api.Application.DTOs.Response;

namespace TaboAni.Api.Application.Common;

public sealed record AuthSessionResult(
    SessionResponseDto Response,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpiresAt,
    bool PersistRefreshToken);
