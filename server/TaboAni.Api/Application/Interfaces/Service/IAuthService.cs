using TaboAni.Api.Application.Common;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;

namespace TaboAni.Api.Application.Interfaces.Service;

public interface IAuthService
{
    Task<AuthSessionResult> LoginAsync(LoginRequestDto loginRequestDto, CancellationToken cancellationToken = default);
    Task<AuthSessionResult> RefreshAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task LogoutAsync(string? refreshToken, CancellationToken cancellationToken = default);
    Task<SignupResponseDto> SignupAsync(SignupRequestDto signupRequestDto, CancellationToken cancellationToken = default);
    Task<ResendEmailVerificationResponseDto> ResendVerificationAsync(
        ResendEmailVerificationRequestDto requestDto,
        CancellationToken cancellationToken = default);
    Task<EmailVerificationStatusResponseDto> VerifyEmailAsync(
        VerifyEmailRequestDto requestDto,
        CancellationToken cancellationToken = default);
}
