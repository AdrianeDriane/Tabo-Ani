using Microsoft.AspNetCore.Mvc;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Service;

namespace TaboAni.Api.Controllers;

[ApiController]
[Route("api/admin/marketplace")]
public sealed class AdminMarketplaceController(IMarketplaceService marketplaceService) : ControllerBase
{
    private readonly IMarketplaceService _marketplaceService = marketplaceService;

    // TODO: Protect this endpoint with proper authentication/authorization when auth middleware is in place.
    [HttpGet("listings")]
    [ProducesResponseType(typeof(ApiResponseDto<PagedAdminMarketplaceListingsResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
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
