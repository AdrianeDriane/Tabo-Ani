using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Application.Interfaces.Service;
using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.Infrastructure.Implementations.Service;

public sealed class MarketplaceService(IUnitOfWork unitOfWork) : IMarketplaceService
{
    private static readonly HashSet<string> SupportedSorts = new(StringComparer.Ordinal)
    {
        "newest",
        "price_asc",
        "price_desc",
        "name_asc",
        "name_desc"
    };

    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Task<IReadOnlyList<ProduceCategoryResponseDto>> GetProduceCategoriesAsync(
        CancellationToken cancellationToken = default)
    {
        return _unitOfWork.Marketplace.GetProduceCategoriesAsync(cancellationToken);
    }

    public async Task<PagedMarketplaceListingsResponseDto> GetPublicListingsAsync(
        MarketplaceListingsQueryRequestDto query,
        CancellationToken cancellationToken = default)
    {
        var validatedQuery = ValidateAndNormalizeQuery(query, allowListingStatusFilter: false);
        var result = await _unitOfWork.Marketplace.GetListingsAsync(validatedQuery, includeAllStatuses: false, cancellationToken);
        var totalPages = CalculateTotalPages(result.TotalCount, result.PageSize);

        return new PagedMarketplaceListingsResponseDto(
            result.Items.Select(item => new MarketplaceListingResponseDto(
                item.ProduceListingId,
                item.ProduceCategoryId,
                item.CategoryName,
                item.FarmerProfileId,
                item.FarmerFarmName,
                item.ListingTitle,
                item.ProduceName,
                item.Description,
                item.PricePerKg,
                item.MinimumOrderKg,
                item.MaximumOrderKg,
                item.PrimaryLocationText,
                item.IsPremiumBoosted,
                item.PrimaryImageUrl,
                item.CreatedAt)).ToList(),
            result.Page,
            result.PageSize,
            result.TotalCount,
            totalPages);
    }

    public async Task<PagedAdminMarketplaceListingsResponseDto> GetAdminListingsAsync(
        MarketplaceListingsQueryRequestDto query,
        CancellationToken cancellationToken = default)
    {
        var validatedQuery = ValidateAndNormalizeQuery(query, allowListingStatusFilter: true);
        var result = await _unitOfWork.Marketplace.GetListingsAsync(validatedQuery, includeAllStatuses: true, cancellationToken);
        var totalPages = CalculateTotalPages(result.TotalCount, result.PageSize);

        return new PagedAdminMarketplaceListingsResponseDto(
            result.Items.Select(item => new AdminMarketplaceListingResponseDto(
                item.ProduceListingId,
                item.ProduceCategoryId,
                item.CategoryName,
                item.FarmerProfileId,
                item.FarmerFarmName,
                item.ListingTitle,
                item.ProduceName,
                item.Description,
                item.PricePerKg,
                item.MinimumOrderKg,
                item.MaximumOrderKg,
                item.PrimaryLocationText,
                item.IsPremiumBoosted,
                item.ListingStatus,
                item.PrimaryImageUrl,
                item.CreatedAt)).ToList(),
            result.Page,
            result.PageSize,
            result.TotalCount,
            totalPages);
    }

    private static MarketplaceListingsQueryRequestDto ValidateAndNormalizeQuery(
        MarketplaceListingsQueryRequestDto? query,
        bool allowListingStatusFilter)
    {
        // Keep validation centralized so controllers stay thin and exceptions stay uniform.
        query ??= new MarketplaceListingsQueryRequestDto(null, null, null, null, null);

        if (query.Page < 1)
        {
            throw new InvalidListingQueryException("Page must be greater than or equal to 1.");
        }

        if (query.PageSize < 1 || query.PageSize > 100)
        {
            throw new InvalidListingQueryException("PageSize must be between 1 and 100.");
        }

        if (query.MinPrice.HasValue && query.MinPrice.Value < 0)
        {
            throw new InvalidListingException("MinPrice cannot be negative.");
        }

        if (query.MaxPrice.HasValue && query.MaxPrice.Value < 0)
        {
            throw new InvalidListingException("MaxPrice cannot be negative.");
        }

        if (query.MinPrice.HasValue && query.MaxPrice.HasValue && query.MaxPrice.Value < query.MinPrice.Value)
        {
            throw new InvalidListingQueryException("MaxPrice must be greater than or equal to MinPrice.");
        }

        var normalizedSort = string.IsNullOrWhiteSpace(query.Sort)
            ? "newest"
            : query.Sort.Trim().ToLowerInvariant();

        if (!SupportedSorts.Contains(normalizedSort))
        {
            throw new InvalidListingQueryException("Sort must be one of: newest, price_asc, price_desc, name_asc, name_desc.");
        }

        var normalizedStatus = allowListingStatusFilter && !string.IsNullOrWhiteSpace(query.ListingStatus)
            ? query.ListingStatus.Trim().ToUpperInvariant()
            : null;

        return query with
        {
            Q = query.Q?.Trim(),
            Location = query.Location?.Trim(),
            Sort = normalizedSort,
            ListingStatus = normalizedStatus
        };
    }

    private static int CalculateTotalPages(int totalCount, int pageSize)
    {
        if (totalCount == 0)
        {
            return 0;
        }

        return (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}
