using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Application.Interfaces.Service;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Exceptions;
using TaboAni.Api.Domain.Validation;

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

    public async Task<FarmerProduceListingDetailResponseDto> CreateListingAsync(
        Guid farmerProfileId,
        CreateProduceListingRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = ValidateFarmerProfileId(farmerProfileId);
        var validatedRequest = ValidateCreateRequest(request);

        await EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);
        await EnsureProduceCategoryExistsAsync(validatedRequest.ProduceCategoryId, cancellationToken);

        var now = DateTimeOffset.UtcNow;
        var listing = BuildListing(validatedFarmerProfileId, validatedRequest, now);

        // TODO: Move image, inventory, and availability management into dedicated listing sub-resources.
        await ExecuteWithinTransactionAsync(async () =>
        {
            await _unitOfWork.Marketplace.AddListingAsync(listing, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return await GetOwnedListingDetailOrThrowAsync(validatedFarmerProfileId, listing.ProduceListingId, cancellationToken);
    }

    public async Task<FarmerProduceListingDetailResponseDto> UpdateListingAsync(
        Guid farmerProfileId,
        Guid listingId,
        UpdateProduceListingRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = ValidateFarmerProfileId(farmerProfileId);
        var validatedListingId = ValidateListingId(listingId);
        var validatedRequest = ValidateUpdateRequest(request);

        await EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);
        await EnsureProduceCategoryExistsAsync(validatedRequest.ProduceCategoryId, cancellationToken);

        var listing = await GetOwnedListingForMutationAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);

        ApplyListingUpdates(listing, validatedRequest, DateTimeOffset.UtcNow);

        await ExecuteWithinTransactionAsync(
            () => _unitOfWork.SaveChangesAsync(cancellationToken),
            cancellationToken);

        return await GetOwnedListingDetailOrThrowAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);
    }

    public async Task<FarmerProduceListingDetailResponseDto> ChangeListingStatusAsync(
        Guid farmerProfileId,
        Guid listingId,
        ChangeProduceListingStatusRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = ValidateFarmerProfileId(farmerProfileId);
        var validatedListingId = ValidateListingId(listingId);
        var validatedRequest = ValidateStatusChangeRequest(request);

        await EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);

        var listing = await GetOwnedListingForMutationAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);
        var nextStatus = ListingStatusPolicy.Normalize(validatedRequest.ListingStatus);

        ListingStatusPolicy.EnsureTransitionAllowed(listing.ListingStatus, nextStatus);

        if (!string.Equals(listing.ListingStatus, nextStatus, StringComparison.Ordinal))
        {
            listing.ListingStatus = nextStatus;
            listing.UpdatedAt = DateTimeOffset.UtcNow;

            await ExecuteWithinTransactionAsync(
                () => _unitOfWork.SaveChangesAsync(cancellationToken),
                cancellationToken);
        }

        return await GetOwnedListingDetailOrThrowAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);
    }

    public async Task<FarmerProduceListingDetailResponseDto> GetFarmerListingDetailAsync(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = ValidateFarmerProfileId(farmerProfileId);
        var validatedListingId = ValidateListingId(listingId);

        await EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);

        return await GetOwnedListingDetailOrThrowAsync(validatedFarmerProfileId, validatedListingId, cancellationToken);
    }

    public async Task<PagedFarmerProduceListingsResponseDto> GetFarmerListingsAsync(
        Guid farmerProfileId,
        FarmerOwnListingsQueryRequestDto query,
        CancellationToken cancellationToken = default)
    {
        var validatedFarmerProfileId = ValidateFarmerProfileId(farmerProfileId);
        var validatedQuery = ValidateAndNormalizeFarmerListingsQuery(query);

        await EnsureFarmerProfileExistsAsync(validatedFarmerProfileId, cancellationToken);

        var result = await _unitOfWork.Marketplace.GetFarmerListingsAsync(validatedFarmerProfileId, validatedQuery, cancellationToken);

        return new PagedFarmerProduceListingsResponseDto(
            result.Items.Select(item => new FarmerProduceListingListItemResponseDto(
                item.ProduceListingId,
                item.ProduceCategoryId,
                item.CategoryName,
                item.ListingTitle,
                item.ProduceName,
                item.PricePerKg,
                item.ListingStatus,
                item.PrimaryLocationText,
                item.IsPremiumBoosted,
                item.PrimaryImageUrl,
                item.CreatedAt,
                item.UpdatedAt)).ToList(),
            result.Page,
            result.PageSize,
            result.TotalCount,
            CalculateTotalPages(result.TotalCount, result.PageSize));
    }

    public async Task<PagedMarketplaceListingsResponseDto> GetPublicListingsAsync(
        MarketplaceListingsQueryRequestDto query,
        CancellationToken cancellationToken = default)
    {
        var validatedQuery = ValidateAndNormalizeMarketplaceQuery(query, allowListingStatusFilter: false);
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
        var validatedQuery = ValidateAndNormalizeMarketplaceQuery(query, allowListingStatusFilter: true);
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

    private async Task ExecuteWithinTransactionAsync(
        Func<Task> action,
        CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            await action();
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private async Task EnsureFarmerProfileExistsAsync(
        Guid farmerProfileId,
        CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Marketplace.FarmerProfileExistsAsync(farmerProfileId, cancellationToken))
        {
            throw new FarmerProfileNotFoundException(farmerProfileId);
        }
    }

    private async Task EnsureProduceCategoryExistsAsync(
        Guid produceCategoryId,
        CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Marketplace.ProduceCategoryExistsAsync(produceCategoryId, cancellationToken))
        {
            throw new ProduceCategoryNotFoundException(produceCategoryId);
        }
    }

    private async Task<ProduceListing> GetOwnedListingForMutationAsync(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken)
    {
        // Load only the tracked listing row needed for mutation and validate ownership in the service boundary.
        var listing = await _unitOfWork.Marketplace.GetListingByIdForUpdateAsync(listingId, cancellationToken)
            ?? throw new ListingNotFoundException(listingId);

        if (listing.FarmerProfileId != farmerProfileId)
        {
            throw new ListingOwnershipException(listingId, farmerProfileId);
        }

        return listing;
    }

    private async Task<FarmerProduceListingDetailResponseDto> GetOwnedListingDetailOrThrowAsync(
        Guid farmerProfileId,
        Guid listingId,
        CancellationToken cancellationToken)
    {
        var detail = await _unitOfWork.Marketplace.GetFarmerListingDetailAsync(farmerProfileId, listingId, cancellationToken);

        if (detail is not null)
        {
            return ToFarmerListingDetailResponse(detail);
        }

        var ownerFarmerProfileId = await _unitOfWork.Marketplace.GetListingOwnerFarmerProfileIdAsync(listingId, cancellationToken);

        if (!ownerFarmerProfileId.HasValue)
        {
            throw new ListingNotFoundException(listingId);
        }

        throw new ListingOwnershipException(listingId, farmerProfileId);
    }

    private static ProduceListing BuildListing(
        Guid farmerProfileId,
        CreateProduceListingRequestDto request,
        DateTimeOffset now)
    {
        return new ProduceListing
        {
            ProduceListingId = Guid.NewGuid(),
            FarmerProfileId = farmerProfileId,
            ProduceCategoryId = request.ProduceCategoryId,
            ListingTitle = request.ListingTitle,
            ProduceName = request.ProduceName,
            Description = request.Description,
            PricePerKg = request.PricePerKg,
            MinimumOrderKg = request.MinimumOrderKg,
            MaximumOrderKg = request.MaximumOrderKg,
            ListingStatus = ListingStatusPolicy.DefaultCreateStatus,
            PrimaryLocationText = request.PrimaryLocationText,
            PrimaryLatitude = request.PrimaryLatitude,
            PrimaryLongitude = request.PrimaryLongitude,
            // TODO: Manage premium boost changes through an admin-only monetization flow.
            IsPremiumBoosted = false,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    private static void ApplyListingUpdates(
        ProduceListing listing,
        UpdateProduceListingRequestDto request,
        DateTimeOffset updatedAt)
    {
        listing.ProduceCategoryId = request.ProduceCategoryId;
        listing.ListingTitle = request.ListingTitle;
        listing.ProduceName = request.ProduceName;
        listing.Description = request.Description;
        listing.PricePerKg = request.PricePerKg;
        listing.MinimumOrderKg = request.MinimumOrderKg;
        listing.MaximumOrderKg = request.MaximumOrderKg;
        listing.PrimaryLocationText = request.PrimaryLocationText;
        listing.PrimaryLatitude = request.PrimaryLatitude;
        listing.PrimaryLongitude = request.PrimaryLongitude;
        listing.UpdatedAt = updatedAt;
    }

    private static Guid ValidateFarmerProfileId(Guid farmerProfileId)
    {
        if (farmerProfileId == Guid.Empty)
        {
            throw new InvalidListingException("FarmerProfileId is required.");
        }

        return farmerProfileId;
    }

    private static Guid ValidateListingId(Guid listingId)
    {
        if (listingId == Guid.Empty)
        {
            throw new InvalidListingException("ListingId is required.");
        }

        return listingId;
    }

    private static CreateProduceListingRequestDto ValidateCreateRequest(CreateProduceListingRequestDto? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidateListingValues(
            request.ProduceCategoryId,
            request.ListingTitle,
            request.ProduceName,
            request.Description,
            request.PricePerKg,
            request.MinimumOrderKg,
            request.MaximumOrderKg,
            request.PrimaryLocationText);

        return request with
        {
            ListingTitle = request.ListingTitle.Trim(),
            ProduceName = request.ProduceName.Trim(),
            Description = request.Description?.Trim(),
            PrimaryLocationText = request.PrimaryLocationText.Trim()
        };
    }

    private static UpdateProduceListingRequestDto ValidateUpdateRequest(UpdateProduceListingRequestDto? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidateListingValues(
            request.ProduceCategoryId,
            request.ListingTitle,
            request.ProduceName,
            request.Description,
            request.PricePerKg,
            request.MinimumOrderKg,
            request.MaximumOrderKg,
            request.PrimaryLocationText);

        return request with
        {
            ListingTitle = request.ListingTitle.Trim(),
            ProduceName = request.ProduceName.Trim(),
            Description = request.Description?.Trim(),
            PrimaryLocationText = request.PrimaryLocationText.Trim()
        };
    }

    private static ChangeProduceListingStatusRequestDto ValidateStatusChangeRequest(ChangeProduceListingStatusRequestDto? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return request with
        {
            ListingStatus = ListingStatusPolicy.Normalize(request.ListingStatus)
        };
    }

    private static FarmerOwnListingsQueryRequestDto ValidateAndNormalizeFarmerListingsQuery(
        FarmerOwnListingsQueryRequestDto? query)
    {
        query ??= new FarmerOwnListingsQueryRequestDto(null, null);

        if (query.Page < 1)
        {
            throw new InvalidListingQueryException("Page must be greater than or equal to 1.");
        }

        if (query.PageSize < 1 || query.PageSize > 100)
        {
            throw new InvalidListingQueryException("PageSize must be between 1 and 100.");
        }

        var normalizedSort = NormalizeSort(query.Sort);
        var normalizedStatus = string.IsNullOrWhiteSpace(query.ListingStatus)
            ? null
            : ListingStatusPolicy.Normalize(query.ListingStatus);

        return query with
        {
            Q = query.Q?.Trim(),
            ListingStatus = normalizedStatus,
            Sort = normalizedSort
        };
    }

    private static MarketplaceListingsQueryRequestDto ValidateAndNormalizeMarketplaceQuery(
        MarketplaceListingsQueryRequestDto? query,
        bool allowListingStatusFilter)
    {
        // Keep query validation centralized so controller actions remain thin and predictable.
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

        var normalizedStatus = allowListingStatusFilter && !string.IsNullOrWhiteSpace(query.ListingStatus)
            ? ListingStatusPolicy.Normalize(query.ListingStatus)
            : null;

        return query with
        {
            Q = query.Q?.Trim(),
            Location = query.Location?.Trim(),
            Sort = NormalizeSort(query.Sort),
            ListingStatus = normalizedStatus
        };
    }

    private static string NormalizeSort(string? sort)
    {
        var normalizedSort = string.IsNullOrWhiteSpace(sort)
            ? "newest"
            : sort.Trim().ToLowerInvariant();

        if (!SupportedSorts.Contains(normalizedSort))
        {
            throw new InvalidListingQueryException("Sort must be one of: newest, price_asc, price_desc, name_asc, name_desc.");
        }

        return normalizedSort;
    }

    private static void ValidateListingValues(
        Guid produceCategoryId,
        string listingTitle,
        string produceName,
        string? description,
        decimal pricePerKg,
        decimal minimumOrderKg,
        decimal? maximumOrderKg,
        string primaryLocationText)
    {
        if (produceCategoryId == Guid.Empty)
        {
            throw new InvalidListingException("ProduceCategoryId is required.");
        }

        if (string.IsNullOrWhiteSpace(listingTitle))
        {
            throw new InvalidListingException("ListingTitle is required.");
        }

        if (string.IsNullOrWhiteSpace(produceName))
        {
            throw new InvalidListingException("ProduceName is required.");
        }

        if (string.IsNullOrWhiteSpace(primaryLocationText))
        {
            throw new InvalidListingException("PrimaryLocationText is required.");
        }

        if (pricePerKg <= 0)
        {
            throw new InvalidListingException("PricePerKg must be greater than 0.");
        }

        if (minimumOrderKg <= 0)
        {
            throw new InvalidListingException("MinimumOrderKg must be greater than 0.");
        }

        if (maximumOrderKg.HasValue && maximumOrderKg.Value < minimumOrderKg)
        {
            throw new InvalidListingException("MaximumOrderKg must be greater than or equal to MinimumOrderKg.");
        }
    }

    private static FarmerProduceListingDetailResponseDto ToFarmerListingDetailResponse(
        FarmerProduceListingDetailQueryResultDto detail)
    {
        return new FarmerProduceListingDetailResponseDto(
            detail.ProduceListingId,
            detail.FarmerProfileId,
            detail.FarmerFarmName,
            detail.ProduceCategoryId,
            detail.CategoryName,
            detail.ListingTitle,
            detail.ProduceName,
            detail.Description,
            detail.PricePerKg,
            detail.MinimumOrderKg,
            detail.MaximumOrderKg,
            detail.ListingStatus,
            detail.PrimaryLocationText,
            detail.PrimaryLatitude,
            detail.PrimaryLongitude,
            detail.IsPremiumBoosted,
            detail.PrimaryImageUrl,
            detail.CreatedAt,
            detail.UpdatedAt);
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
