using Microsoft.EntityFrameworkCore;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Data;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Infrastructure.Implementations.Repository;

public sealed class MarketplaceRepository(AppDbContext context) : IMarketplaceRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IReadOnlyList<ProduceCategoryResponseDto>> GetProduceCategoriesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.ProduceCategories
            .AsNoTracking()
            .OrderBy(category => category.CategoryName)
            .Select(category => new ProduceCategoryResponseDto(
                category.ProduceCategoryId,
                category.CategoryName,
                category.Description))
            .ToListAsync(cancellationToken);
    }

    public async Task<PagedMarketplaceListingQueryResultDto> GetListingsAsync(
        MarketplaceListingsQueryRequestDto query,
        bool includeAllStatuses,
        CancellationToken cancellationToken = default)
    {
        // Build a single SQL query to avoid N+1 lookups across related marketplace data.
        IQueryable<ProduceListing> listingQuery = _context.ProduceListings.AsNoTracking();

        if (!includeAllStatuses)
        {
            listingQuery = listingQuery.Where(listing => listing.ListingStatus == "ACTIVE");
        }
        else if (!string.IsNullOrWhiteSpace(query.ListingStatus))
        {
            listingQuery = listingQuery.Where(listing => listing.ListingStatus == query.ListingStatus);
        }

        if (!string.IsNullOrWhiteSpace(query.Q))
        {
            var searchPattern = $"%{query.Q}%";
            listingQuery = listingQuery.Where(listing =>
                EF.Functions.ILike(listing.ProduceName, searchPattern) ||
                EF.Functions.ILike(listing.ListingTitle, searchPattern));
        }

        if (query.CategoryId.HasValue)
        {
            listingQuery = listingQuery.Where(listing => listing.ProduceCategoryId == query.CategoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.Location))
        {
            var locationPattern = $"%{query.Location}%";
            listingQuery = listingQuery.Where(listing =>
                EF.Functions.ILike(listing.PrimaryLocationText, locationPattern));
        }

        if (query.MinPrice.HasValue)
        {
            listingQuery = listingQuery.Where(listing => listing.PricePerKg >= query.MinPrice.Value);
        }

        if (query.MaxPrice.HasValue)
        {
            listingQuery = listingQuery.Where(listing => listing.PricePerKg <= query.MaxPrice.Value);
        }

        var totalCount = await listingQuery.CountAsync(cancellationToken);
        var skip = (query.Page - 1) * query.PageSize;

        var orderedQuery = ApplyOrdering(listingQuery, query.Sort);

        var items = await orderedQuery
            .Skip(skip)
            .Take(query.PageSize)
            .Select(listing => new MarketplaceListingQueryResultItemDto(
                listing.ProduceListingId,
                listing.ProduceCategoryId,
                _context.ProduceCategories
                    .Where(category => category.ProduceCategoryId == listing.ProduceCategoryId)
                    .Select(category => category.CategoryName)
                    .FirstOrDefault() ?? string.Empty,
                listing.FarmerProfileId,
                _context.FarmerProfiles
                    .Where(profile => profile.FarmerProfileId == listing.FarmerProfileId)
                    .Select(profile => profile.FarmName)
                    .FirstOrDefault() ?? string.Empty,
                listing.ListingTitle,
                listing.ProduceName,
                listing.Description,
                listing.PricePerKg,
                listing.MinimumOrderKg,
                listing.MaximumOrderKg,
                listing.PrimaryLocationText,
                listing.IsPremiumBoosted,
                listing.ListingStatus,
                _context.ProduceListingImages
                    .Where(image => image.ProduceListingId == listing.ProduceListingId)
                    .OrderByDescending(image => image.IsPrimary)
                    .ThenBy(image => image.DisplayOrder)
                    .Select(image => image.ImageUrl)
                    .FirstOrDefault(),
                listing.CreatedAt))
            .ToListAsync(cancellationToken);

        return new PagedMarketplaceListingQueryResultDto(items, query.Page, query.PageSize, totalCount);
    }

    private static IOrderedQueryable<ProduceListing> ApplyOrdering(
        IQueryable<ProduceListing> listingQuery,
        string? sort)
    {
        var normalizedSort = string.IsNullOrWhiteSpace(sort) ? "newest" : sort;

        // Premium boosted listings are always ranked first for marketplace discoverability.
        return normalizedSort switch
        {
            "price_asc" => listingQuery
                .OrderByDescending(listing => listing.IsPremiumBoosted)
                .ThenBy(listing => listing.PricePerKg)
                .ThenByDescending(listing => listing.CreatedAt)
                .ThenByDescending(listing => listing.ProduceListingId),
            "price_desc" => listingQuery
                .OrderByDescending(listing => listing.IsPremiumBoosted)
                .ThenByDescending(listing => listing.PricePerKg)
                .ThenByDescending(listing => listing.CreatedAt)
                .ThenByDescending(listing => listing.ProduceListingId),
            "name_asc" => listingQuery
                .OrderByDescending(listing => listing.IsPremiumBoosted)
                .ThenBy(listing => listing.ProduceName)
                .ThenByDescending(listing => listing.CreatedAt)
                .ThenByDescending(listing => listing.ProduceListingId),
            "name_desc" => listingQuery
                .OrderByDescending(listing => listing.IsPremiumBoosted)
                .ThenByDescending(listing => listing.ProduceName)
                .ThenByDescending(listing => listing.CreatedAt)
                .ThenByDescending(listing => listing.ProduceListingId),
            _ => listingQuery
                .OrderByDescending(listing => listing.IsPremiumBoosted)
                .ThenByDescending(listing => listing.CreatedAt)
                .ThenByDescending(listing => listing.ProduceListingId)
        };
    }
}
