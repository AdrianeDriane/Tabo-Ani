using Mapster;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Enums;

namespace TaboAni.Api.Application.Configuration.MapsterConfiguration;

public sealed class MarketplaceMapsterConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ListingStatus, string>()
            .MapWith(status => status.ToString().ToUpperInvariant());

        config.NewConfig<FarmerProduceListingDetailQueryResultDto, FarmerProduceListingDetailResponseDto>();
        config.NewConfig<FarmerProduceListingListItemQueryResultDto, FarmerProduceListingListItemResponseDto>();
        config.NewConfig<MarketplaceListingQueryResultItemDto, MarketplaceListingResponseDto>();
        config.NewConfig<MarketplaceListingQueryResultItemDto, AdminMarketplaceListingResponseDto>();
        config.NewConfig<InventoryBatchQueryResultDto, InventoryBatchResponseDto>();
        config.NewConfig<ProduceInventoryBatch, InventoryBatchResponseDto>();
        config.NewConfig<ListingAllowedVehicleTypeQueryResultDto, ListingAllowedVehicleTypeResponseDto>();
    }
}
