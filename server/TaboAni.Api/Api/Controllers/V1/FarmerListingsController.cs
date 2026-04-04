using Microsoft.AspNetCore.Mvc;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Service;

namespace TaboAni.Api.Controllers.V1;

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

    [HttpPost("{listingId:guid}/vehicle-types")]
    [ProducesResponseType(typeof(ApiResponseDto<ListingAllowedVehicleTypesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AssignAllowedVehicleType(
        Guid farmerProfileId,
        Guid listingId,
        [FromBody] AssignListingVehicleTypeRequestDto request,
        CancellationToken cancellationToken)
    {
        var allowedVehicleTypes = await _marketplaceService.AssignAllowedVehicleTypeAsync(
            farmerProfileId,
            listingId,
            request,
            cancellationToken);

        return Ok(new ApiResponseDto<ListingAllowedVehicleTypesResponseDto>
        {
            Success = true,
            Message = "Allowed vehicle type assigned successfully.",
            Data = allowedVehicleTypes
        });
    }

    [HttpDelete("{listingId:guid}/vehicle-types/{vehicleTypeId:guid}")]
    [ProducesResponseType(typeof(ApiResponseDto<ListingAllowedVehicleTypesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveAllowedVehicleType(
        Guid farmerProfileId,
        Guid listingId,
        Guid vehicleTypeId,
        CancellationToken cancellationToken)
    {
        var allowedVehicleTypes = await _marketplaceService.RemoveAllowedVehicleTypeAsync(
            farmerProfileId,
            listingId,
            vehicleTypeId,
            cancellationToken);

        return Ok(new ApiResponseDto<ListingAllowedVehicleTypesResponseDto>
        {
            Success = true,
            Message = "Allowed vehicle type removed successfully.",
            Data = allowedVehicleTypes
        });
    }

    [HttpGet("{listingId:guid}/vehicle-types")]
    [ProducesResponseType(typeof(ApiResponseDto<ListingAllowedVehicleTypesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllowedVehicleTypes(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken)
    {
        var allowedVehicleTypes = await _marketplaceService.GetAllowedVehicleTypesAsync(
            farmerProfileId,
            listingId,
            cancellationToken);

        return Ok(new ApiResponseDto<ListingAllowedVehicleTypesResponseDto>
        {
            Success = true,
            Message = "Allowed vehicle types retrieved successfully.",
            Data = allowedVehicleTypes
        });
    }

    [HttpPost("{listingId:guid}/inventory-batches")]
    [ProducesResponseType(typeof(ApiResponseDto<InventoryBatchResponseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateInventoryBatch(
        Guid farmerProfileId,
        Guid listingId,
        [FromBody] CreateInventoryBatchRequestDto request,
        CancellationToken cancellationToken)
    {
        var batch = await _marketplaceService.CreateInventoryBatchAsync(
            farmerProfileId,
            listingId,
            request,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetListingInventory),
            new { farmerProfileId, listingId },
            new ApiResponseDto<InventoryBatchResponseDto>
            {
                Success = true,
                Message = "Inventory batch created successfully.",
                Data = batch
            });
    }

    [HttpPut("{listingId:guid}/inventory-batches/{batchId:guid}")]
    [ProducesResponseType(typeof(ApiResponseDto<InventoryBatchResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateInventoryBatch(
        Guid farmerProfileId,
        Guid listingId,
        Guid batchId,
        [FromBody] UpdateInventoryBatchRequestDto request,
        CancellationToken cancellationToken)
    {
        var batch = await _marketplaceService.UpdateInventoryBatchAsync(
            farmerProfileId,
            listingId,
            batchId,
            request,
            cancellationToken);

        return Ok(new ApiResponseDto<InventoryBatchResponseDto>
        {
            Success = true,
            Message = "Inventory batch updated successfully.",
            Data = batch
        });
    }

    [HttpGet("{listingId:guid}/inventory")]
    [ProducesResponseType(typeof(ApiResponseDto<FarmerListingInventoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListingInventory(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken)
    {
        var inventory = await _marketplaceService.GetListingInventoryAsync(farmerProfileId, listingId, cancellationToken);

        return Ok(new ApiResponseDto<FarmerListingInventoryResponseDto>
        {
            Success = true,
            Message = "Listing inventory retrieved successfully.",
            Data = inventory
        });
    }

    [HttpPatch("{listingId:guid}/price")]
    [ProducesResponseType(typeof(ApiResponseDto<FarmerProduceListingDetailResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateListingPrice(
        Guid farmerProfileId,
        Guid listingId,
        [FromBody] UpdateListingPriceRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _marketplaceService.UpdateListingPriceAsync(
            farmerProfileId,
            listingId,
            request,
            cancellationToken);

        return Ok(new ApiResponseDto<FarmerProduceListingDetailResponseDto>
        {
            Success = true,
            Message = result.PriceChanged
                ? "Listing price updated successfully."
                : "Listing price unchanged.",
            Data = result.Listing
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
