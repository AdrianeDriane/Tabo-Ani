using Microsoft.EntityFrameworkCore;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Data;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Validation;

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
        // Build a single SQL query so marketplace list reads do not fan out into N+1 sub-queries per listing.
        IQueryable<ProduceListing> listingQuery = _context.ProduceListings.AsNoTracking();

        if (!includeAllStatuses)
        {
            listingQuery = listingQuery.Where(listing => listing.ListingStatus == ListingStatusPolicy.Active);
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

        return await GetMarketplaceListingPageAsync(listingQuery, query.Page, query.PageSize, query.Sort, cancellationToken);
    }

    public Task<bool> FarmerProfileExistsAsync(Guid farmerProfileId, CancellationToken cancellationToken = default)
    {
        return _context.FarmerProfiles
            .AsNoTracking()
            .AnyAsync(profile => profile.FarmerProfileId == farmerProfileId, cancellationToken);
    }

    public Task<bool> ProduceCategoryExistsAsync(Guid produceCategoryId, CancellationToken cancellationToken = default)
    {
        return _context.ProduceCategories
            .AsNoTracking()
            .AnyAsync(category => category.ProduceCategoryId == produceCategoryId, cancellationToken);
    }

    public Task AddListingAsync(ProduceListing listing, CancellationToken cancellationToken = default)
    {
        return _context.ProduceListings.AddAsync(listing, cancellationToken).AsTask();
    }

    public Task<ProduceListing?> GetListingByIdForUpdateAsync(Guid listingId, CancellationToken cancellationToken = default)
    {
        return _context.ProduceListings
            .SingleOrDefaultAsync(listing => listing.ProduceListingId == listingId, cancellationToken);
    }

    public Task<Guid?> GetListingOwnerFarmerProfileIdAsync(Guid listingId, CancellationToken cancellationToken = default)
    {
        return _context.ProduceListings
            .AsNoTracking()
            .Where(listing => listing.ProduceListingId == listingId)
            .Select(listing => (Guid?)listing.FarmerProfileId)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public Task<FarmerProduceListingDetailQueryResultDto?> GetFarmerListingDetailAsync(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken = default)
    {
        return _context.ProduceListings
            .AsNoTracking()
            .Where(listing => listing.FarmerProfileId == farmerProfileId && listing.ProduceListingId == listingId)
            .Select(listing => new FarmerProduceListingDetailQueryResultDto(
                listing.ProduceListingId,
                listing.FarmerProfileId,
                _context.FarmerProfiles
                    .Where(profile => profile.FarmerProfileId == listing.FarmerProfileId)
                    .Select(profile => profile.FarmName)
                    .FirstOrDefault() ?? string.Empty,
                listing.ProduceCategoryId,
                _context.ProduceCategories
                    .Where(category => category.ProduceCategoryId == listing.ProduceCategoryId)
                    .Select(category => category.CategoryName)
                    .FirstOrDefault() ?? string.Empty,
                listing.ListingTitle,
                listing.ProduceName,
                listing.Description,
                listing.PricePerKg,
                listing.MinimumOrderKg,
                listing.MaximumOrderKg,
                listing.ListingStatus,
                listing.PrimaryLocationText,
                listing.PrimaryLatitude,
                listing.PrimaryLongitude,
                listing.IsPremiumBoosted,
                _context.ProduceListingImages
                    .Where(image => image.ProduceListingId == listing.ProduceListingId)
                    .OrderByDescending(image => image.IsPrimary)
                    .ThenBy(image => image.DisplayOrder)
                    .Select(image => image.ImageUrl)
                    .FirstOrDefault(),
                listing.CreatedAt,
                listing.UpdatedAt))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedFarmerProduceListingQueryResultDto> GetFarmerListingsAsync(
        Guid farmerProfileId,
        FarmerOwnListingsQueryRequestDto query,
        CancellationToken cancellationToken = default)
    {
        IQueryable<ProduceListing> listingQuery = _context.ProduceListings
            .AsNoTracking()
            .Where(listing => listing.FarmerProfileId == farmerProfileId);

        if (!string.IsNullOrWhiteSpace(query.Q))
        {
            var searchPattern = $"%{query.Q}%";
            listingQuery = listingQuery.Where(listing =>
                EF.Functions.ILike(listing.ProduceName, searchPattern) ||
                EF.Functions.ILike(listing.ListingTitle, searchPattern));
        }

        if (!string.IsNullOrWhiteSpace(query.ListingStatus))
        {
            listingQuery = listingQuery.Where(listing => listing.ListingStatus == query.ListingStatus);
        }

        var totalCount = await listingQuery.CountAsync(cancellationToken);
        var skip = (query.Page - 1) * query.PageSize;
        var orderedQuery = ApplyOrdering(listingQuery, query.Sort);

        var items = await orderedQuery
            .Skip(skip)
            .Take(query.PageSize)
            .Select(listing => new FarmerProduceListingListItemQueryResultDto(
                listing.ProduceListingId,
                listing.ProduceCategoryId,
                _context.ProduceCategories
                    .Where(category => category.ProduceCategoryId == listing.ProduceCategoryId)
                    .Select(category => category.CategoryName)
                    .FirstOrDefault() ?? string.Empty,
                listing.ListingTitle,
                listing.ProduceName,
                listing.PricePerKg,
                listing.ListingStatus,
                listing.PrimaryLocationText,
                listing.IsPremiumBoosted,
                _context.ProduceListingImages
                    .Where(image => image.ProduceListingId == listing.ProduceListingId)
                    .OrderByDescending(image => image.IsPrimary)
                    .ThenBy(image => image.DisplayOrder)
                    .Select(image => image.ImageUrl)
                    .FirstOrDefault(),
                listing.CreatedAt,
                listing.UpdatedAt))
            .ToListAsync(cancellationToken);

        return new PagedFarmerProduceListingQueryResultDto(items, query.Page, query.PageSize, totalCount);
    }

    private async Task<PagedMarketplaceListingQueryResultDto> GetMarketplaceListingPageAsync(
        IQueryable<ProduceListing> listingQuery,
        int page,
        int pageSize,
        string? sort,
        CancellationToken cancellationToken)
    {
        var totalCount = await listingQuery.CountAsync(cancellationToken);
        var skip = (page - 1) * pageSize;
        var orderedQuery = ApplyOrdering(listingQuery, sort);

        var items = await orderedQuery
            .Skip(skip)
            .Take(pageSize)
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

        return new PagedMarketplaceListingQueryResultDto(items, page, pageSize, totalCount);
    }

    private static IOrderedQueryable<ProduceListing> ApplyOrdering(
        IQueryable<ProduceListing> listingQuery,
        string? sort)
    {
        var normalizedSort = string.IsNullOrWhiteSpace(sort) ? "newest" : sort;

        // Premium boosted listings stay ranked first across both public and owner list views.
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
