using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TaboAni.Api.Application.Common;
using TaboAni.Api.Application.Configuration;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Service;
using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.Controllers.V1;

[ApiController]
[Route("api/v1/auth")]
public sealed class AuthController(
    IAuthService authService,
    IOptions<AuthOptions> authOptions) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly AuthOptions _authOptions = authOptions.Value;

    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponseDto<SessionResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
    {
        var session = await _authService.LoginAsync(loginRequestDto, cancellationToken);
        AppendRefreshTokenCookie(session);

        return Ok(new ApiResponseDto<SessionResponseDto>
        {
            Success = true,
            Message = "Login successful.",
            Data = session.Response
        });
    }

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(ApiResponseDto<SessionResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
    {
        var refreshToken = Request.Cookies[_authOptions.RefreshTokenCookieName];

        try
        {
            var session = await _authService.RefreshAsync(refreshToken ?? string.Empty, cancellationToken);
            AppendRefreshTokenCookie(session);

            return Ok(new ApiResponseDto<SessionResponseDto>
            {
                Success = true,
                Message = "Session refreshed successfully.",
                Data = session.Response
            });
        }
        catch (InvalidRefreshTokenException)
        {
            ClearRefreshTokenCookie();
            throw;
        }
        catch (AccountStatusNotAllowedException)
        {
            ClearRefreshTokenCookie();
            throw;
        }
    }

    [HttpPost("logout")]
    [ProducesResponseType(typeof(ApiResponseDto<LogoutResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        var refreshToken = Request.Cookies[_authOptions.RefreshTokenCookieName];
        await _authService.LogoutAsync(refreshToken, cancellationToken);
        ClearRefreshTokenCookie();

        return Ok(new ApiResponseDto<LogoutResponseDto>
        {
            Success = true,
            Message = "Logout successful.",
            Data = new LogoutResponseDto()
        });
    }

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

    private void AppendRefreshTokenCookie(AuthSessionResult session)
    {
        var cookieOptions = CreateRefreshTokenCookieOptions(session.PersistRefreshToken, session.RefreshTokenExpiresAt);

        Response.Cookies.Append(_authOptions.RefreshTokenCookieName, session.RefreshToken, cookieOptions);
    }

    private void ClearRefreshTokenCookie()
    {
        Response.Cookies.Delete(
            _authOptions.RefreshTokenCookieName,
            new CookieOptions
            {
                Path = _authOptions.RefreshTokenCookiePath,
                HttpOnly = true,
                SameSite = ResolveSameSiteMode(_authOptions.RefreshTokenCookieSameSite),
                Secure = _authOptions.UseSecureRefreshTokenCookie,
                IsEssential = true
            });
    }

    private CookieOptions CreateRefreshTokenCookieOptions(bool persistRefreshToken, DateTimeOffset refreshTokenExpiresAt)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = _authOptions.UseSecureRefreshTokenCookie,
            SameSite = ResolveSameSiteMode(_authOptions.RefreshTokenCookieSameSite),
            Path = _authOptions.RefreshTokenCookiePath,
            IsEssential = true
        };

        if (persistRefreshToken)
        {
            cookieOptions.Expires = refreshTokenExpiresAt;
        }

        return cookieOptions;
    }

    private static SameSiteMode ResolveSameSiteMode(string configuredValue)
    {
        return Enum.TryParse<SameSiteMode>(configuredValue, true, out var sameSiteMode)
            ? sameSiteMode
            : SameSiteMode.None;
    }
}
