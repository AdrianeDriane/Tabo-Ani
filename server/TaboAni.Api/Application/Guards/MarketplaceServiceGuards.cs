using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.Application.Guards;

internal sealed class MarketplaceServiceGuards(IUnitOfWork unitOfWork)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task EnsureFarmerProfileExistsAsync(
        Guid farmerProfileId,
        CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Marketplace.FarmerProfileExistsAsync(farmerProfileId, cancellationToken))
        {
            throw new FarmerProfileNotFoundException(farmerProfileId);
        }
    }

    public async Task EnsureProduceCategoryExistsAsync(
        Guid produceCategoryId,
        CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Marketplace.ProduceCategoryExistsAsync(produceCategoryId, cancellationToken))
        {
            throw new ProduceCategoryNotFoundException(produceCategoryId);
        }
    }

    public async Task EnsureVehicleTypeExistsAsync(
        Guid vehicleTypeId,
        CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Marketplace.VehicleTypeExistsAsync(vehicleTypeId, cancellationToken))
        {
            throw new VehicleTypeNotFoundException(vehicleTypeId);
        }
    }

    public async Task<ProduceListing> GetOwnedListingForMutationAsync(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken)
    {
        var listing = await _unitOfWork.Marketplace.GetListingByIdForUpdateAsync(listingId, cancellationToken)
            ?? throw new ListingNotFoundException(listingId);

        if (listing.FarmerProfileId != farmerProfileId)
        {
            throw new ListingOwnershipException(listingId, farmerProfileId);
        }

        return listing;
    }

    public async Task<ProduceInventoryBatch> GetOwnedInventoryBatchForMutationAsync(
        Guid listingId,
        Guid batchId,
        CancellationToken cancellationToken)
    {
        var inventoryBatch = await _unitOfWork.Marketplace.GetInventoryBatchByIdForUpdateAsync(batchId, cancellationToken)
            ?? throw new InventoryBatchNotFoundException(batchId);

        if (inventoryBatch.ProduceListingId != listingId)
        {
            throw new InventoryBatchListingMismatchException(batchId, listingId);
        }

        return inventoryBatch;
    }

    public async Task<FarmerProduceListingDetailQueryResultDto> GetOwnedListingDetailQueryOrThrowAsync(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken)
    {
        var detail = await _unitOfWork.Marketplace.GetFarmerListingDetailAsync(farmerProfileId, listingId, cancellationToken);

        if (detail is not null)
        {
            return detail;
        }

        await ThrowForMissingOwnedListingAsync(farmerProfileId, listingId, cancellationToken);
        throw new InvalidOperationException("Owned listing resolution should always throw before reaching this line.");
    }

    public async Task ThrowForMissingOwnedListingAsync(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken)
    {
        var ownerFarmerProfileId = await _unitOfWork.Marketplace.GetListingOwnerFarmerProfileIdAsync(listingId, cancellationToken);

        if (!ownerFarmerProfileId.HasValue)
        {
            throw new ListingNotFoundException(listingId);
        }

        throw new ListingOwnershipException(listingId, farmerProfileId);
    }

    public async Task EnsureInventoryBatchCodeIsUniqueAsync(
        Guid listingId,
        string? batchCode,
        Guid? excludeBatchId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(batchCode))
        {
            return;
        }

        if (await _unitOfWork.Marketplace.IsInventoryBatchCodeInUseAsync(
                listingId,
                batchCode,
                excludeBatchId,
                cancellationToken))
        {
            throw new InvalidInventoryBatchException("BatchCode must be unique within the listing.");
        }
    }
}
