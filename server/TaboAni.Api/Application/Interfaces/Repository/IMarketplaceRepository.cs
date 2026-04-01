using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;

namespace TaboAni.Api.Application.Interfaces.Repository;

public interface IMarketplaceRepository
{
    Task<IReadOnlyList<ProduceCategoryResponseDto>> GetProduceCategoriesAsync(
        CancellationToken cancellationToken = default);

    Task<PagedMarketplaceListingQueryResultDto> GetListingsAsync(
        MarketplaceListingsQueryRequestDto query,
        bool includeAllStatuses,
        CancellationToken cancellationToken = default);
}
