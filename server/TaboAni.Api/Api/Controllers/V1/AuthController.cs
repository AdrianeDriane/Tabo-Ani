using Microsoft.AspNetCore.Mvc;
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
    [ProducesResponseType(typeof(ApiResponseDto<SignupResponseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Signup([FromBody] SignupRequestDto signupRequestDto, CancellationToken cancellationToken)
    {
        var signupResponse = await _authService.SignupAsync(signupRequestDto, cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            new ApiResponseDto<SignupResponseDto>
            {
                Success = true,
                Message = "Signup completed successfully.",
                Data = signupResponse
            });
    }
}
