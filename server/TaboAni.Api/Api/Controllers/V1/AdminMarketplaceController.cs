using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaboAni.Api.Api.Authorization;
using TaboAni.Api.Application.Configuration;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Service;

namespace TaboAni.Api.Controllers.V1;

[ApiController]
[RequireRoles(RoleCodes.Admin)]
[Route("api/v1/admin/marketplace")]
public sealed class AdminMarketplaceController(IMarketplaceService marketplaceService) : ControllerBase
{
    private readonly IMarketplaceService _marketplaceService = marketplaceService;

    [HttpGet("listings")]
    [ProducesResponseType(typeof(ApiResponseDto<PagedAdminMarketplaceListingsResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListings(
        [FromQuery] MarketplaceListingsQueryRequestDto query,
        CancellationToken cancellationToken)
    {
        var listings = await _marketplaceService.GetAdminListingsAsync(query, cancellationToken);

        return Ok(new ApiResponseDto<PagedAdminMarketplaceListingsResponseDto>
        {
            Success = true,
            Message = "Admin marketplace listings retrieved successfully.",
            Data = listings
        });
    }
}
