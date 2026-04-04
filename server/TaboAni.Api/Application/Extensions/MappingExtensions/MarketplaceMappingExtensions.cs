using Mapster;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Application.Extensions.MappingExtensions;

public static class MarketplaceMappingExtensions
{
    public static FarmerProduceListingDetailResponseDto ToResponseDto(this FarmerProduceListingDetailQueryResultDto detail)
    {
        return detail.Adapt<FarmerProduceListingDetailResponseDto>();
    }

    public static FarmerProduceListingListItemResponseDto ToResponseDto(this FarmerProduceListingListItemQueryResultDto item)
    {
        return item.Adapt<FarmerProduceListingListItemResponseDto>();
    }

    public static MarketplaceListingResponseDto ToResponseDto(this MarketplaceListingQueryResultItemDto item)
    {
        return item.Adapt<MarketplaceListingResponseDto>();
    }

    public static AdminMarketplaceListingResponseDto ToAdminResponseDto(this MarketplaceListingQueryResultItemDto item)
    {
        return item.Adapt<AdminMarketplaceListingResponseDto>();
    }

    public static InventoryBatchResponseDto ToResponseDto(this InventoryBatchQueryResultDto batch)
    {
        return batch.Adapt<InventoryBatchResponseDto>();
    }

    public static InventoryBatchResponseDto ToResponseDto(this ProduceInventoryBatch batch)
    {
        return batch.Adapt<InventoryBatchResponseDto>();
    }

    public static ListingAllowedVehicleTypeResponseDto ToResponseDto(this ListingAllowedVehicleTypeQueryResultDto vehicleType)
    {
        return vehicleType.Adapt<ListingAllowedVehicleTypeResponseDto>();
    }

    public static ListingAllowedVehicleTypesResponseDto ToAllowedVehicleTypesResponseDto(
        this IReadOnlyList<ListingAllowedVehicleTypeQueryResultDto> allowedVehicleTypes,
        Guid produceListingId)
    {
        return new ListingAllowedVehicleTypesResponseDto(
            produceListingId,
            allowedVehicleTypes.Select(vehicleType => vehicleType.ToResponseDto()).ToList());
    }

    public static FarmerListingInventoryResponseDto ToResponseDto(this FarmerListingInventoryQueryResultDto inventory)
    {
        var batches = inventory.Batches
            .Select(batch => batch.ToResponseDto())
            .ToList();

        return new FarmerListingInventoryResponseDto(
            inventory.ProduceListingId,
            inventory.ListingTitle,
            inventory.ProduceName,
            batches.Sum(batch => batch.AvailableQuantityKg),
            batches.Sum(batch => batch.ReservedQuantityKg),
            batches);
    }
}
