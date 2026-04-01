using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;

namespace TaboAni.Api.Application.Interfaces.Service;

public interface IMarketplaceService
{
    Task<IReadOnlyList<ProduceCategoryResponseDto>> GetProduceCategoriesAsync(
        CancellationToken cancellationToken = default);

    Task<FarmerProduceListingDetailResponseDto> CreateListingAsync(
        Guid farmerProfileId,
        CreateProduceListingRequestDto request,
        CancellationToken cancellationToken = default);

    Task<FarmerProduceListingDetailResponseDto> UpdateListingAsync(
        Guid farmerProfileId,
        Guid listingId,
        UpdateProduceListingRequestDto request,
        CancellationToken cancellationToken = default);

    Task<FarmerProduceListingDetailResponseDto> ChangeListingStatusAsync(
        Guid farmerProfileId,
        Guid listingId,
        ChangeProduceListingStatusRequestDto request,
        CancellationToken cancellationToken = default);

    Task<FarmerProduceListingDetailResponseDto> GetFarmerListingDetailAsync(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken = default);

    Task<PagedFarmerProduceListingsResponseDto> GetFarmerListingsAsync(
        Guid farmerProfileId,
        FarmerOwnListingsQueryRequestDto query,
        CancellationToken cancellationToken = default);

    Task<PagedMarketplaceListingsResponseDto> GetPublicListingsAsync(
        MarketplaceListingsQueryRequestDto query,
        CancellationToken cancellationToken = default);

    Task<PagedAdminMarketplaceListingsResponseDto> GetAdminListingsAsync(
        MarketplaceListingsQueryRequestDto query,
        CancellationToken cancellationToken = default);
}
