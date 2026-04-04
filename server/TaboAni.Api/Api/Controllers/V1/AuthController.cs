using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Service;

namespace TaboAni.Api.Controllers.V1;

[ApiController]
[Route("api/v1/auth")]
public sealed class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("signup")]
    [EnableRateLimiting("auth-signup")]
    [ProducesResponseType(typeof(ApiResponseDto<SignupResponseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Signup([FromBody] SignupRequestDto signupRequestDto, CancellationToken cancellationToken)
    {
        var signupResponse = await _authService.SignupAsync(signupRequestDto, cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            new ApiResponseDto<SignupResponseDto>
            {
                Success = true,
                Message = "Account created. Check your email to verify your account.",
                Data = signupResponse
            });
    }

    [HttpPost("verify-email/resend")]
    [EnableRateLimiting("auth-resend-verification")]
    [ProducesResponseType(typeof(ApiResponseDto<ResendEmailVerificationResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ResendVerification(
        [FromBody] ResendEmailVerificationRequestDto requestDto,
        CancellationToken cancellationToken)
    {
        var verificationStatus = await _authService.ResendVerificationAsync(requestDto, cancellationToken);

        return Ok(new ApiResponseDto<ResendEmailVerificationResponseDto>
        {
            Success = true,
            Message = "If the account is eligible, a verification link will be emailed shortly.",
            Data = verificationStatus
        });
    }

    [HttpPost("verify-email")]
    [EnableRateLimiting("auth-verify-email")]
    [ProducesResponseType(typeof(ApiResponseDto<EmailVerificationStatusResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> VerifyEmail(
        [FromBody] VerifyEmailRequestDto requestDto,
        CancellationToken cancellationToken)
    {
        var verificationStatus = await _authService.VerifyEmailAsync(requestDto, cancellationToken);

        return Ok(new ApiResponseDto<EmailVerificationStatusResponseDto>
        {
            Success = true,
            Message = "Email verification status retrieved successfully.",
            Data = verificationStatus
        });
    }
}
