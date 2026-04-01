using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Application.Interfaces.Repository;

public interface IMarketplaceRepository
{
    Task<IReadOnlyList<ProduceCategoryResponseDto>> GetProduceCategoriesAsync(
        CancellationToken cancellationToken = default);

    Task<PagedMarketplaceListingQueryResultDto> GetListingsAsync(
        MarketplaceListingsQueryRequestDto query,
        bool includeAllStatuses,
        CancellationToken cancellationToken = default);

    Task<bool> FarmerProfileExistsAsync(Guid farmerProfileId, CancellationToken cancellationToken = default);
    Task<bool> ProduceCategoryExistsAsync(Guid produceCategoryId, CancellationToken cancellationToken = default);
    Task AddListingAsync(ProduceListing listing, CancellationToken cancellationToken = default);
    Task<ProduceListing?> GetListingByIdForUpdateAsync(Guid listingId, CancellationToken cancellationToken = default);
    Task<Guid?> GetListingOwnerFarmerProfileIdAsync(Guid listingId, CancellationToken cancellationToken = default);

    Task<FarmerProduceListingDetailQueryResultDto?> GetFarmerListingDetailAsync(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken = default);

    Task<PagedFarmerProduceListingQueryResultDto> GetFarmerListingsAsync(
        Guid farmerProfileId,
        FarmerOwnListingsQueryRequestDto query,
        CancellationToken cancellationToken = default);
}
