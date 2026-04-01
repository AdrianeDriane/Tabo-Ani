using Microsoft.AspNetCore.Mvc;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Service;

namespace TaboAni.Api.Controllers;

[ApiController]
[Route("api/v1/farmers/{farmerProfileId:guid}/listings")]
public sealed class FarmerListingsController(IMarketplaceService marketplaceService) : ControllerBase
{
    private readonly IMarketplaceService _marketplaceService = marketplaceService;

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseDto<FarmerProduceListingDetailResponseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateListing(
        Guid farmerProfileId,
        [FromBody] CreateProduceListingRequestDto request,
        CancellationToken cancellationToken)
    {
        var listing = await _marketplaceService.CreateListingAsync(farmerProfileId, request, cancellationToken);

        return CreatedAtAction(
            nameof(GetListingDetail),
            new { farmerProfileId, listingId = listing.ProduceListingId },
            new ApiResponseDto<FarmerProduceListingDetailResponseDto>
            {
                Success = true,
                Message = "Produce listing created successfully.",
                Data = listing
            });
    }

    [HttpPut("{listingId:guid}")]
    [ProducesResponseType(typeof(ApiResponseDto<FarmerProduceListingDetailResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateListing(
        Guid farmerProfileId,
        Guid listingId,
        [FromBody] UpdateProduceListingRequestDto request,
        CancellationToken cancellationToken)
    {
        var listing = await _marketplaceService.UpdateListingAsync(farmerProfileId, listingId, request, cancellationToken);

        return Ok(new ApiResponseDto<FarmerProduceListingDetailResponseDto>
        {
            Success = true,
            Message = "Produce listing updated successfully.",
            Data = listing
        });
    }

    [HttpPatch("{listingId:guid}/status")]
    [ProducesResponseType(typeof(ApiResponseDto<FarmerProduceListingDetailResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangeListingStatus(
        Guid farmerProfileId,
        Guid listingId,
        [FromBody] ChangeProduceListingStatusRequestDto request,
        CancellationToken cancellationToken)
    {
        var listing = await _marketplaceService.ChangeListingStatusAsync(farmerProfileId, listingId, request, cancellationToken);

        return Ok(new ApiResponseDto<FarmerProduceListingDetailResponseDto>
        {
            Success = true,
            Message = "Produce listing status updated successfully.",
            Data = listing
        });
    }

    [HttpGet("{listingId:guid}")]
    [ProducesResponseType(typeof(ApiResponseDto<FarmerProduceListingDetailResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListingDetail(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken)
    {
        var listing = await _marketplaceService.GetFarmerListingDetailAsync(farmerProfileId, listingId, cancellationToken);

        return Ok(new ApiResponseDto<FarmerProduceListingDetailResponseDto>
        {
            Success = true,
            Message = "Produce listing retrieved successfully.",
            Data = listing
        });
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseDto<PagedFarmerProduceListingsResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFarmerListings(
        Guid farmerProfileId,
        [FromQuery] FarmerOwnListingsQueryRequestDto query,
        CancellationToken cancellationToken)
    {
        var listings = await _marketplaceService.GetFarmerListingsAsync(farmerProfileId, query, cancellationToken);

        return Ok(new ApiResponseDto<PagedFarmerProduceListingsResponseDto>
        {
            Success = true,
            Message = "Farmer produce listings retrieved successfully.",
            Data = listings
        });
    }
}
