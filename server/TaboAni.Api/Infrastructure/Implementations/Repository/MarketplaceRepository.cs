using Microsoft.EntityFrameworkCore;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Data;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Enums;

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
            listingQuery = listingQuery.Where(listing => listing.ListingStatus == ListingStatus.Active);
        }
        else if (!string.IsNullOrWhiteSpace(query.ListingStatus))
        {
            var listingStatusFilter = Enum.Parse<ListingStatus>(query.ListingStatus, true);
            listingQuery = listingQuery.Where(listing => listing.ListingStatus == listingStatusFilter);
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

    public Task AddListingVehicleTypeAsync(
        FarmerListingVehicleType listingVehicleType,
        CancellationToken cancellationToken = default)
    {
        return _context.FarmerListingVehicleTypes.AddAsync(listingVehicleType, cancellationToken).AsTask();
    }

    public Task AddInventoryBatchAsync(
        ProduceInventoryBatch inventoryBatch,
        CancellationToken cancellationToken = default)
    {
        return _context.ProduceInventoryBatches.AddAsync(inventoryBatch, cancellationToken).AsTask();
    }

    public Task AddListingPriceHistoryAsync(
        ListingPriceHistory priceHistory,
        CancellationToken cancellationToken = default)
    {
        return _context.ListingPriceHistory.AddAsync(priceHistory, cancellationToken).AsTask();
    }

    public async Task<bool> RemoveListingVehicleTypeAsync(
        Guid listingId,
        Guid vehicleTypeId,
        CancellationToken cancellationToken = default)
    {
        var listingVehicleType = await _context.FarmerListingVehicleTypes
            .SingleOrDefaultAsync(
                mapping => mapping.ProduceListingId == listingId && mapping.VehicleTypeId == vehicleTypeId,
                cancellationToken);

        if (listingVehicleType is null)
        {
            return false;
        }

        _context.FarmerListingVehicleTypes.Remove(listingVehicleType);
        return true;
    }

    public Task<ProduceListing?> GetListingByIdForUpdateAsync(Guid listingId, CancellationToken cancellationToken = default)
    {
        return _context.ProduceListings
            .SingleOrDefaultAsync(listing => listing.ProduceListingId == listingId, cancellationToken);
    }

    public Task<ProduceInventoryBatch?> GetInventoryBatchByIdForUpdateAsync(
        Guid batchId,
        CancellationToken cancellationToken = default)
    {
        return _context.ProduceInventoryBatches
            .SingleOrDefaultAsync(batch => batch.ProduceInventoryBatchId == batchId, cancellationToken);
    }

    public Task<Guid?> GetListingOwnerFarmerProfileIdAsync(Guid listingId, CancellationToken cancellationToken = default)
    {
        return _context.ProduceListings
            .AsNoTracking()
            .Where(listing => listing.ProduceListingId == listingId)
            .Select(listing => (Guid?)listing.FarmerProfileId)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public Task<bool> VehicleTypeExistsAsync(Guid vehicleTypeId, CancellationToken cancellationToken = default)
    {
        return _context.VehicleTypes
            .AsNoTracking()
            .AnyAsync(vehicleType => vehicleType.VehicleTypeId == vehicleTypeId, cancellationToken);
    }

    public Task<bool> IsVehicleTypeAllowedForListingAsync(
        Guid listingId,
        Guid vehicleTypeId,
        CancellationToken cancellationToken = default)
    {
        return _context.FarmerListingVehicleTypes
            .AsNoTracking()
            .AnyAsync(
                mapping => mapping.ProduceListingId == listingId && mapping.VehicleTypeId == vehicleTypeId,
                cancellationToken);
    }

    public Task<bool> IsInventoryBatchCodeInUseAsync(
        Guid listingId,
        string batchCode,
        Guid? excludeBatchId,
        CancellationToken cancellationToken = default)
    {
        return _context.ProduceInventoryBatches
            .AsNoTracking()
            .AnyAsync(
                batch => batch.ProduceListingId == listingId &&
                         batch.BatchCode == batchCode &&
                         (!excludeBatchId.HasValue || batch.ProduceInventoryBatchId != excludeBatchId.Value),
                cancellationToken);
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

    public async Task<FarmerListingInventoryQueryResultDto?> GetListingInventoryAsync(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken = default)
    {
        var listing = await _context.ProduceListings
            .AsNoTracking()
            .Where(produceListing =>
                produceListing.FarmerProfileId == farmerProfileId &&
                produceListing.ProduceListingId == listingId)
            .Select(produceListing => new
            {
                produceListing.ProduceListingId,
                produceListing.ListingTitle,
                produceListing.ProduceName
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (listing is null)
        {
            return null;
        }

        // Keep inventory reads bounded to two SQL queries: one for the listing header and one for the batch projection.
        var batches = await _context.ProduceInventoryBatches
            .AsNoTracking()
            .Where(batch => batch.ProduceListingId == listingId)
            .OrderByDescending(batch => batch.UpdatedAt)
            .ThenBy(batch => batch.BatchCode)
            .ThenBy(batch => batch.ProduceInventoryBatchId)
            .Select(batch => new InventoryBatchQueryResultDto(
                batch.ProduceInventoryBatchId,
                batch.ProduceListingId,
                batch.BatchCode,
                batch.EstimatedHarvestDate,
                batch.ActualHarvestDate,
                batch.AvailableQuantityKg,
                batch.ReservedQuantityKg,
                batch.InventoryStatus,
                batch.Notes,
                batch.CreatedAt,
                batch.UpdatedAt))
            .ToListAsync(cancellationToken);

        return new FarmerListingInventoryQueryResultDto(
            listing.ProduceListingId,
            listing.ListingTitle,
            listing.ProduceName,
            batches);
    }

    public async Task<IReadOnlyList<ListingAllowedVehicleTypeQueryResultDto>?> GetListingAllowedVehicleTypesAsync(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken = default)
    {
        var isOwnedListing = await _context.ProduceListings
            .AsNoTracking()
            .AnyAsync(
                listing => listing.ProduceListingId == listingId && listing.FarmerProfileId == farmerProfileId,
                cancellationToken);

        if (!isOwnedListing)
        {
            return null;
        }

        var allowedVehicleTypes = await _context.FarmerListingVehicleTypes
            .AsNoTracking()
            .Where(mapping => mapping.ProduceListingId == listingId)
            .Join(
                _context.VehicleTypes.AsNoTracking(),
                mapping => mapping.VehicleTypeId,
                vehicleType => vehicleType.VehicleTypeId,
                (_, vehicleType) => new ListingAllowedVehicleTypeQueryResultDto(
                    vehicleType.VehicleTypeId,
                    vehicleType.VehicleTypeName,
                    vehicleType.Description,
                    vehicleType.MaxCapacityKg,
                    vehicleType.IsActive))
            .OrderBy(vehicleType => vehicleType.VehicleTypeName)
            .ThenBy(vehicleType => vehicleType.VehicleTypeId)
            .ToListAsync(cancellationToken);

        return allowedVehicleTypes;
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
            var listingStatusFilter = Enum.Parse<ListingStatus>(query.ListingStatus, true);
            listingQuery = listingQuery.Where(listing => listing.ListingStatus == listingStatusFilter);
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
