using Microsoft.AspNetCore.Mvc;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Service;

namespace TaboAni.Api.Controllers;

[ApiController]
[Route("api/v1/marketplace")]
public sealed class MarketplaceController(IMarketplaceService marketplaceService) : ControllerBase
{
    private readonly IMarketplaceService _marketplaceService = marketplaceService;

    [HttpGet("categories")]
    [ProducesResponseType(typeof(ApiResponseDto<IReadOnlyList<ProduceCategoryResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProduceCategories(CancellationToken cancellationToken)
    {
        var categories = await _marketplaceService.GetProduceCategoriesAsync(cancellationToken);

        return Ok(new ApiResponseDto<IReadOnlyList<ProduceCategoryResponseDto>>
        {
            Success = true,
            Message = "Produce categories retrieved successfully.",
            Data = categories
        });
    }

    [HttpGet("listings")]
    [ProducesResponseType(typeof(ApiResponseDto<PagedMarketplaceListingsResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListings(
        [FromQuery] MarketplaceListingsQueryRequestDto query,
        CancellationToken cancellationToken)
    {
        var listings = await _marketplaceService.GetPublicListingsAsync(query, cancellationToken);

        return Ok(new ApiResponseDto<PagedMarketplaceListingsResponseDto>
        {
            Success = true,
            Message = "Marketplace listings retrieved successfully.",
            Data = listings
        });
    }
}
