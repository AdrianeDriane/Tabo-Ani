using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Domain.Enums;
using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.Application.Validation.Marketplace;

internal static class MarketplaceValidationHelper
{
    private static readonly HashSet<string> SupportedSorts = new(StringComparer.Ordinal)
    {
        "newest",
        "price_asc",
        "price_desc",
        "name_asc",
        "name_desc"
    };

    public static Guid ValidateFarmerProfileId(Guid farmerProfileId)
    {
        if (farmerProfileId == Guid.Empty)
        {
            throw new InvalidListingException("FarmerProfileId is required.");
        }

        return farmerProfileId;
    }

    public static Guid ValidateListingId(Guid listingId)
    {
        if (listingId == Guid.Empty)
        {
            throw new InvalidListingException("ListingId is required.");
        }

        return listingId;
    }

    public static Guid ValidateInventoryBatchId(Guid batchId)
    {
        if (batchId == Guid.Empty)
        {
            throw new InvalidInventoryBatchException("ProduceInventoryBatchId is required.");
        }

        return batchId;
    }

    public static Guid ValidateVehicleTypeId(Guid vehicleTypeId)
    {
        if (vehicleTypeId == Guid.Empty)
        {
            throw new InvalidListingException("VehicleTypeId is required.");
        }

        return vehicleTypeId;
    }

    public static CreateProduceListingRequestDto ValidateCreateRequest(CreateProduceListingRequestDto? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return request with
        {
            ListingTitle = request.ListingTitle.Trim(),
            ProduceName = request.ProduceName.Trim(),
            Description = request.Description?.Trim(),
            PrimaryLocationText = request.PrimaryLocationText.Trim()
        };
    }

    public static UpdateProduceListingRequestDto ValidateUpdateRequest(UpdateProduceListingRequestDto? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return request with
        {
            ListingTitle = request.ListingTitle.Trim(),
            ProduceName = request.ProduceName.Trim(),
            Description = request.Description?.Trim(),
            PrimaryLocationText = request.PrimaryLocationText.Trim()
        };
    }

    public static ChangeProduceListingStatusRequestDto ValidateStatusChangeRequest(ChangeProduceListingStatusRequestDto? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        _ = ParseListingStatusOrThrow(request.ListingStatus);

        return request with
        {
            ListingStatus = request.ListingStatus.Trim()
        };
    }

    public static AssignListingVehicleTypeRequestDto ValidateAssignVehicleTypeRequest(AssignListingVehicleTypeRequestDto? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return request with
        {
            VehicleTypeId = ValidateVehicleTypeId(request.VehicleTypeId)
        };
    }

    public static CreateInventoryBatchRequestDto ValidateCreateInventoryBatchRequest(CreateInventoryBatchRequestDto? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return request with
        {
            BatchCode = NormalizeOptionalText(request.BatchCode),
            Notes = NormalizeOptionalText(request.Notes)
        };
    }

    public static UpdateInventoryBatchRequestDto ValidateUpdateInventoryBatchRequest(UpdateInventoryBatchRequestDto? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return request with
        {
            BatchCode = NormalizeOptionalText(request.BatchCode),
            Notes = NormalizeOptionalText(request.Notes)
        };
    }

    public static UpdateListingPriceRequestDto ValidatePriceUpdateRequest(UpdateListingPriceRequestDto? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return request;
    }

    public static FarmerOwnListingsQueryRequestDto ValidateAndNormalizeFarmerListingsQuery(
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
            : ToListingStatusWireValue(ParseListingStatusOrThrow(query.ListingStatus));

        return query with
        {
            Q = query.Q?.Trim(),
            ListingStatus = normalizedStatus,
            Sort = normalizedSort
        };
    }

    public static MarketplaceListingsQueryRequestDto ValidateAndNormalizeMarketplaceQuery(
        MarketplaceListingsQueryRequestDto? query,
        bool allowListingStatusFilter)
    {
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
            ? ToListingStatusWireValue(ParseListingStatusOrThrow(query.ListingStatus))
            : null;

        return query with
        {
            Q = query.Q?.Trim(),
            Location = query.Location?.Trim(),
            Sort = NormalizeSort(query.Sort),
            ListingStatus = normalizedStatus
        };
    }

    public static int CalculateTotalPages(int totalCount, int pageSize)
    {
        if (totalCount == 0)
        {
            return 0;
        }

        return (int)Math.Ceiling(totalCount / (double)pageSize);
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

    private static string? NormalizeOptionalText(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    public static ListingStatus ParseListingStatusOrThrow(string? listingStatus)
    {
        if (string.IsNullOrWhiteSpace(listingStatus))
        {
            throw new InvalidListingException("ListingStatus is required.");
        }

        if (!Enum.TryParse<ListingStatus>(listingStatus.Trim(), true, out var parsedStatus))
        {
            throw new InvalidListingException("ListingStatus must be one of: ACTIVE, INACTIVE, ARCHIVED.");
        }

        return parsedStatus;
    }

    public static string ToListingStatusWireValue(ListingStatus listingStatus)
    {
        return listingStatus.ToString().ToUpperInvariant();
    }
}
