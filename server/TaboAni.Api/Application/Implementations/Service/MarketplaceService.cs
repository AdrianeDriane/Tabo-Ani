using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Extensions.MappingExtensions;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Application.Interfaces.Service;
using TaboAni.Api.Application.Common;
using TaboAni.Api.Application.Guards;
using TaboAni.Api.Application.Validation.Marketplace;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.Application.Implementations.Service;

public sealed class MarketplaceService(IUnitOfWork unitOfWork) : IMarketplaceService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly MarketplaceServiceGuards _guards = new(unitOfWork);

    public Task<IReadOnlyList<ProduceCategoryResponseDto>> GetProduceCategoriesAsync(
        CancellationToken cancellationToken = default)
    {
        return _unitOfWork.Marketplace.GetProduceCategoriesAsync(cancellationToken);
    }

    public async Task<FarmerProduceListingDetailResponseDto> CreateListingAsync(
        Guid farmerProfileId,
        CreateProduceListingRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = MarketplaceValidationHelper.ValidateFarmerProfileId(farmerProfileId);
        var validatedRequest = MarketplaceValidationHelper.ValidateCreateRequest(request);

        await _guards.EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);
        await _guards.EnsureProduceCategoryExistsAsync(validatedRequest.ProduceCategoryId, cancellationToken);

        var listing = ProduceListing.Create(
            validatedFarmerProfileId,
            validatedRequest.ProduceCategoryId,
            validatedRequest.ListingTitle,
            validatedRequest.ProduceName,
            validatedRequest.Description,
            validatedRequest.PricePerKg,
            validatedRequest.MinimumOrderKg,
            validatedRequest.MaximumOrderKg,
            validatedRequest.PrimaryLocationText,
            validatedRequest.PrimaryLatitude,
            validatedRequest.PrimaryLongitude,
            DateTimeOffset.UtcNow);

        await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(_unitOfWork, async () =>
        {
            await _unitOfWork.Marketplace.AddListingAsync(listing, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        var detail = await _guards.GetOwnedListingDetailQueryOrThrowAsync(
            validatedFarmerProfileId,
            listing.ProduceListingId,
            cancellationToken);

        return detail.ToResponseDto();
    }

    public async Task<FarmerProduceListingDetailResponseDto> UpdateListingAsync(
        Guid farmerProfileId,
        Guid listingId,
        UpdateProduceListingRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = MarketplaceValidationHelper.ValidateFarmerProfileId(farmerProfileId);
        var validatedListingId = MarketplaceValidationHelper.ValidateListingId(listingId);
        var validatedRequest = MarketplaceValidationHelper.ValidateUpdateRequest(request);

        await _guards.EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);
        await _guards.EnsureProduceCategoryExistsAsync(validatedRequest.ProduceCategoryId, cancellationToken);

        var listing = await _guards.GetOwnedListingForMutationAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);
        var updatedAt = DateTimeOffset.UtcNow;
        var oldPrice = listing.PricePerKg;

        listing.UpdateDetails(
            validatedRequest.ProduceCategoryId,
            validatedRequest.ListingTitle,
            validatedRequest.ProduceName,
            validatedRequest.Description,
            validatedRequest.PricePerKg,
            validatedRequest.MinimumOrderKg,
            validatedRequest.MaximumOrderKg,
            validatedRequest.PrimaryLocationText,
            validatedRequest.PrimaryLatitude,
            validatedRequest.PrimaryLongitude,
            updatedAt);

        ListingPriceHistory? priceHistory = null;
        if (oldPrice != listing.PricePerKg)
        {
            priceHistory = listing.CreatePriceHistory(oldPrice, listing.PricePerKg, updatedAt);
        }

        await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(_unitOfWork, async () =>
        {
            if (priceHistory is not null)
            {
                await _unitOfWork.Marketplace.AddListingPriceHistoryAsync(priceHistory, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        var detail = await _guards.GetOwnedListingDetailQueryOrThrowAsync(
            validatedFarmerProfileId,
            validatedListingId,
            cancellationToken);

        return detail.ToResponseDto();
    }

    public async Task<FarmerProduceListingDetailResponseDto> ChangeListingStatusAsync(
        Guid farmerProfileId,
        Guid listingId,
        ChangeProduceListingStatusRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = MarketplaceValidationHelper.ValidateFarmerProfileId(farmerProfileId);
        var validatedListingId = MarketplaceValidationHelper.ValidateListingId(listingId);
        var validatedRequest = MarketplaceValidationHelper.ValidateStatusChangeRequest(request);

        await _guards.EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);

        var listing = await _guards.GetOwnedListingForMutationAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);
        var nextStatus = MarketplaceValidationHelper.ParseListingStatusOrThrow(validatedRequest.ListingStatus);
        var beforeStatus = listing.ListingStatus;
        listing.ChangeStatus(nextStatus, DateTimeOffset.UtcNow);

        if (beforeStatus != listing.ListingStatus)
        {
            await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(
                _unitOfWork,
                () => _unitOfWork.SaveChangesAsync(cancellationToken),
                cancellationToken);
        }

        var detail = await _guards.GetOwnedListingDetailQueryOrThrowAsync(
            validatedFarmerProfileId,
            validatedListingId,
            cancellationToken);

        return detail.ToResponseDto();
    }

    public async Task<FarmerProduceListingDetailResponseDto> GetFarmerListingDetailAsync(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = MarketplaceValidationHelper.ValidateFarmerProfileId(farmerProfileId);
        var validatedListingId = MarketplaceValidationHelper.ValidateListingId(listingId);

        await _guards.EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);

        var detail = await _guards.GetOwnedListingDetailQueryOrThrowAsync(
            validatedFarmerProfileId,
            validatedListingId,
            cancellationToken);

        return detail.ToResponseDto();
    }

    public async Task<ListingAllowedVehicleTypesResponseDto> AssignAllowedVehicleTypeAsync(
        Guid farmerProfileId,
        Guid listingId,
        AssignListingVehicleTypeRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = MarketplaceValidationHelper.ValidateFarmerProfileId(farmerProfileId);
        var validatedListingId = MarketplaceValidationHelper.ValidateListingId(listingId);
        var validatedRequest = MarketplaceValidationHelper.ValidateAssignVehicleTypeRequest(request);

        await _guards.EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);
        await _guards.GetOwnedListingForMutationAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);
        await _guards.EnsureVehicleTypeExistsAsync(validatedRequest.VehicleTypeId, cancellationToken);

        if (await _unitOfWork.Marketplace.IsVehicleTypeAllowedForListingAsync(
                validatedListingId,
                validatedRequest.VehicleTypeId,
                cancellationToken))
        {
            throw new ListingVehicleTypeAlreadyAssignedException(validatedListingId, validatedRequest.VehicleTypeId);
        }

        await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(_unitOfWork, async () =>
        {
            await _unitOfWork.Marketplace.AddListingVehicleTypeAsync(
                FarmerListingVehicleType.Create(
                    validatedListingId,
                    validatedRequest.VehicleTypeId,
                    DateTimeOffset.UtcNow),
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return await GetAllowedVehicleTypesAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);
    }

    public async Task<ListingAllowedVehicleTypesResponseDto> RemoveAllowedVehicleTypeAsync(
        Guid farmerProfileId,
        Guid listingId,
        Guid vehicleTypeId,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = MarketplaceValidationHelper.ValidateFarmerProfileId(farmerProfileId);
        var validatedListingId = MarketplaceValidationHelper.ValidateListingId(listingId);
        var validatedVehicleTypeId = MarketplaceValidationHelper.ValidateVehicleTypeId(vehicleTypeId);

        await _guards.EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);
        await _guards.GetOwnedListingForMutationAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);
        await _guards.EnsureVehicleTypeExistsAsync(validatedVehicleTypeId, cancellationToken);

        await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(_unitOfWork, async () =>
        {
            var removed = await _unitOfWork.Marketplace.RemoveListingVehicleTypeAsync(
                validatedListingId,
                validatedVehicleTypeId,
                cancellationToken);

            if (!removed)
            {
                throw new ListingVehicleTypeNotAssignedException(validatedListingId, validatedVehicleTypeId);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return await GetAllowedVehicleTypesAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);
    }

    public async Task<ListingAllowedVehicleTypesResponseDto> GetAllowedVehicleTypesAsync(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = MarketplaceValidationHelper.ValidateFarmerProfileId(farmerProfileId);
        var validatedListingId = MarketplaceValidationHelper.ValidateListingId(listingId);

        await _guards.EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);

        var allowedVehicleTypes = await _unitOfWork.Marketplace.GetListingAllowedVehicleTypesAsync(
            validatedFarmerProfileId,
            validatedListingId,
            cancellationToken);

        if (allowedVehicleTypes is null)
        {
            await _guards.ThrowForMissingOwnedListingAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);
            throw new InvalidOperationException("Owned listing validation should have thrown before reaching this line.");
        }

        return allowedVehicleTypes.ToAllowedVehicleTypesResponseDto(validatedListingId);
    }

    public async Task<InventoryBatchResponseDto> CreateInventoryBatchAsync(
        Guid farmerProfileId,
        Guid listingId,
        CreateInventoryBatchRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = MarketplaceValidationHelper.ValidateFarmerProfileId(farmerProfileId);
        var validatedListingId = MarketplaceValidationHelper.ValidateListingId(listingId);
        var validatedRequest = MarketplaceValidationHelper.ValidateCreateInventoryBatchRequest(request);

        await _guards.EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);
        await _guards.GetOwnedListingForMutationAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);
        await _guards.EnsureInventoryBatchCodeIsUniqueAsync(validatedListingId, validatedRequest.BatchCode, null, cancellationToken);

        var inventoryBatch = ProduceInventoryBatch.Create(
            validatedListingId,
            validatedRequest.BatchCode,
            validatedRequest.EstimatedHarvestDate,
            validatedRequest.ActualHarvestDate,
            validatedRequest.AvailableQuantityKg,
            validatedRequest.ReservedQuantityKg,
            validatedRequest.Notes,
            DateTimeOffset.UtcNow);

        await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(_unitOfWork, async () =>
        {
            await _unitOfWork.Marketplace.AddInventoryBatchAsync(inventoryBatch, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return inventoryBatch.ToResponseDto();
    }

    public async Task<InventoryBatchResponseDto> UpdateInventoryBatchAsync(
        Guid farmerProfileId,
        Guid listingId,
        Guid batchId,
        UpdateInventoryBatchRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = MarketplaceValidationHelper.ValidateFarmerProfileId(farmerProfileId);
        var validatedListingId = MarketplaceValidationHelper.ValidateListingId(listingId);
        var validatedBatchId = MarketplaceValidationHelper.ValidateInventoryBatchId(batchId);
        var validatedRequest = MarketplaceValidationHelper.ValidateUpdateInventoryBatchRequest(request);

        await _guards.EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);
        await _guards.GetOwnedListingForMutationAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);

        var inventoryBatch = await _guards.GetOwnedInventoryBatchForMutationAsync(validatedListingId, validatedBatchId, cancellationToken);
        await _guards.EnsureInventoryBatchCodeIsUniqueAsync(
            validatedListingId,
            validatedRequest.BatchCode,
            validatedBatchId,
            cancellationToken);

        inventoryBatch.UpdateBatch(
            validatedRequest.BatchCode,
            validatedRequest.EstimatedHarvestDate,
            validatedRequest.ActualHarvestDate,
            validatedRequest.AvailableQuantityKg,
            validatedRequest.ReservedQuantityKg,
            validatedRequest.Notes,
            DateTimeOffset.UtcNow);

        await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(
            _unitOfWork,
            () => _unitOfWork.SaveChangesAsync(cancellationToken),
            cancellationToken);

        return inventoryBatch.ToResponseDto();
    }

    public async Task<FarmerListingInventoryResponseDto> GetListingInventoryAsync(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = MarketplaceValidationHelper.ValidateFarmerProfileId(farmerProfileId);
        var validatedListingId = MarketplaceValidationHelper.ValidateListingId(listingId);

        await _guards.EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);

        var inventory = await _unitOfWork.Marketplace.GetListingInventoryAsync(
            validatedFarmerProfileId,
            validatedListingId,
            cancellationToken);

        if (inventory is null)
        {
            await _guards.ThrowForMissingOwnedListingAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);
        }

        return inventory!.ToResponseDto();
    }

    public async Task<ListingPriceUpdateResultDto> UpdateListingPriceAsync(
        Guid farmerProfileId,
        Guid listingId,
        UpdateListingPriceRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = MarketplaceValidationHelper.ValidateFarmerProfileId(farmerProfileId);
        var validatedListingId = MarketplaceValidationHelper.ValidateListingId(listingId);
        var validatedRequest = MarketplaceValidationHelper.ValidatePriceUpdateRequest(request);

        await _guards.EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);

        var listing = await _guards.GetOwnedListingForMutationAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);
        var oldPrice = listing.PricePerKg;
        var priceChanged = listing.TryChangePrice(validatedRequest.PricePerKg, DateTimeOffset.UtcNow);

        if (priceChanged)
        {
            var priceHistory = listing.CreatePriceHistory(oldPrice, listing.PricePerKg, listing.UpdatedAt);

            await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(_unitOfWork, async () =>
            {
                await _unitOfWork.Marketplace.AddListingPriceHistoryAsync(priceHistory, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }, cancellationToken);
        }

        var detail = await _guards.GetOwnedListingDetailQueryOrThrowAsync(
            validatedFarmerProfileId,
            validatedListingId,
            cancellationToken);

        return new ListingPriceUpdateResultDto(detail.ToResponseDto(), priceChanged);
    }

    public async Task<PagedFarmerProduceListingsResponseDto> GetFarmerListingsAsync(
        Guid farmerProfileId,
        FarmerOwnListingsQueryRequestDto query,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = MarketplaceValidationHelper.ValidateFarmerProfileId(farmerProfileId);
        var validatedQuery = MarketplaceValidationHelper.ValidateAndNormalizeFarmerListingsQuery(query);

        await _guards.EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);

        var result = await _unitOfWork.Marketplace.GetFarmerListingsAsync(validatedFarmerProfileId, validatedQuery, cancellationToken);

        return new PagedFarmerProduceListingsResponseDto(
            result.Items.Select(item => item.ToResponseDto()).ToList(),
            result.Page,
            result.PageSize,
            result.TotalCount,
            MarketplaceValidationHelper.CalculateTotalPages(result.TotalCount, result.PageSize));
    }

    public async Task<PagedMarketplaceListingsResponseDto> GetPublicListingsAsync(
        MarketplaceListingsQueryRequestDto query,
        CancellationToken cancellationToken = default)
    {
        var validatedQuery = MarketplaceValidationHelper.ValidateAndNormalizeMarketplaceQuery(query, allowListingStatusFilter: false);
        var result = await _unitOfWork.Marketplace.GetListingsAsync(validatedQuery, includeAllStatuses: false, cancellationToken);

        return new PagedMarketplaceListingsResponseDto(
            result.Items.Select(item => item.ToResponseDto()).ToList(),
            result.Page,
            result.PageSize,
            result.TotalCount,
            MarketplaceValidationHelper.CalculateTotalPages(result.TotalCount, result.PageSize));
    }

    public async Task<PagedAdminMarketplaceListingsResponseDto> GetAdminListingsAsync(
        MarketplaceListingsQueryRequestDto query,
        CancellationToken cancellationToken = default)
    {
        var validatedQuery = MarketplaceValidationHelper.ValidateAndNormalizeMarketplaceQuery(query, allowListingStatusFilter: true);
        var result = await _unitOfWork.Marketplace.GetListingsAsync(validatedQuery, includeAllStatuses: true, cancellationToken);

        return new PagedAdminMarketplaceListingsResponseDto(
            result.Items.Select(item => item.ToAdminResponseDto()).ToList(),
            result.Page,
            result.PageSize,
            result.TotalCount,
            MarketplaceValidationHelper.CalculateTotalPages(result.TotalCount, result.PageSize));
    }
}
