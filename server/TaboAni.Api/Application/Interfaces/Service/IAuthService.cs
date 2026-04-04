using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;

namespace TaboAni.Api.Application.Interfaces.Service;

public interface IAuthService
{
    Task<SignupResponseDto> SignupAsync(SignupRequestDto signupRequestDto, CancellationToken cancellationToken = default);
    Task<ResendEmailVerificationResponseDto> ResendVerificationAsync(
        ResendEmailVerificationRequestDto requestDto,
        CancellationToken cancellationToken = default);
    Task<EmailVerificationStatusResponseDto> VerifyEmailAsync(
        VerifyEmailRequestDto requestDto,
        CancellationToken cancellationToken = default);
}
