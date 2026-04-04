using TaboAni.Api.Domain.Exceptions;
using TaboAni.Api.Domain.Enums;

namespace TaboAni.Api.Domain.Entities;

public class ProduceListing
{
    public Guid ProduceListingId { get; set; }
    public Guid FarmerProfileId { get; set; }
    public Guid ProduceCategoryId { get; set; }
    public string ListingTitle { get; set; } = string.Empty;
    public string ProduceName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal PricePerKg { get; set; }
    public decimal MinimumOrderKg { get; set; }
    public decimal? MaximumOrderKg { get; set; }
    public ListingStatus ListingStatus { get; set; }
    public string PrimaryLocationText { get; set; } = string.Empty;
    public decimal? PrimaryLatitude { get; set; }
    public decimal? PrimaryLongitude { get; set; }
    public bool IsPremiumBoosted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public ICollection<FarmerListingVehicleType> AllowedVehicleTypes { get; set; } = new List<FarmerListingVehicleType>();

    public static ProduceListing Create(
        Guid farmerProfileId,
        Guid produceCategoryId,
        string listingTitle,
        string produceName,
        string? description,
        decimal pricePerKg,
        decimal minimumOrderKg,
        decimal? maximumOrderKg,
        string primaryLocationText,
        decimal? primaryLatitude,
        decimal? primaryLongitude,
        DateTimeOffset now)
    {
        EnsureCoreValues(
            produceCategoryId,
            listingTitle,
            produceName,
            pricePerKg,
            minimumOrderKg,
            maximumOrderKg,
            primaryLocationText);

        if (farmerProfileId == Guid.Empty)
        {
            throw new InvalidListingException("FarmerProfileId is required.");
        }

        return new ProduceListing
        {
            ProduceListingId = Guid.NewGuid(),
            FarmerProfileId = farmerProfileId,
            ProduceCategoryId = produceCategoryId,
            ListingTitle = listingTitle,
            ProduceName = produceName,
            Description = description,
            PricePerKg = pricePerKg,
            MinimumOrderKg = minimumOrderKg,
            MaximumOrderKg = maximumOrderKg,
            ListingStatus = ListingStatus.Active,
            PrimaryLocationText = primaryLocationText,
            PrimaryLatitude = primaryLatitude,
            PrimaryLongitude = primaryLongitude,
            IsPremiumBoosted = false,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    public void UpdateDetails(
        Guid produceCategoryId,
        string listingTitle,
        string produceName,
        string? description,
        decimal pricePerKg,
        decimal minimumOrderKg,
        decimal? maximumOrderKg,
        string primaryLocationText,
        decimal? primaryLatitude,
        decimal? primaryLongitude,
        DateTimeOffset updatedAt)
    {
        EnsureCoreValues(
            produceCategoryId,
            listingTitle,
            produceName,
            pricePerKg,
            minimumOrderKg,
            maximumOrderKg,
            primaryLocationText);

        ProduceCategoryId = produceCategoryId;
        ListingTitle = listingTitle;
        ProduceName = produceName;
        Description = description;
        PricePerKg = pricePerKg;
        MinimumOrderKg = minimumOrderKg;
        MaximumOrderKg = maximumOrderKg;
        PrimaryLocationText = primaryLocationText;
        PrimaryLatitude = primaryLatitude;
        PrimaryLongitude = primaryLongitude;
        UpdatedAt = updatedAt;
    }

    public void ChangeStatus(ListingStatus nextStatus, DateTimeOffset updatedAt)
    {
        EnsureStatusTransitionAllowed(ListingStatus, nextStatus);
        if (ListingStatus != nextStatus)
        {
            ListingStatus = nextStatus;
            UpdatedAt = updatedAt;
        }
    }

    public void Activate(DateTimeOffset updatedAt)
    {
        ChangeStatus(ListingStatus.Active, updatedAt);
    }

    public void Deactivate(DateTimeOffset updatedAt)
    {
        ChangeStatus(ListingStatus.Inactive, updatedAt);
    }

    public void Archive(DateTimeOffset updatedAt)
    {
        ChangeStatus(ListingStatus.Archived, updatedAt);
    }

    public bool TryChangePrice(decimal nextPricePerKg, DateTimeOffset updatedAt)
    {
        if (nextPricePerKg <= 0)
        {
            throw new InvalidListingPriceException("PricePerKg must be greater than 0.");
        }

        if (PricePerKg == nextPricePerKg)
        {
            return false;
        }

        PricePerKg = nextPricePerKg;
        UpdatedAt = updatedAt;
        return true;
    }

    public ListingPriceHistory CreatePriceHistory(decimal oldPricePerKg, decimal newPricePerKg, DateTimeOffset effectiveAt)
    {
        return ListingPriceHistory.Create(ProduceListingId, oldPricePerKg, newPricePerKg, effectiveAt);
    }

    private static void EnsureCoreValues(
        Guid produceCategoryId,
        string listingTitle,
        string produceName,
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

    private static void EnsureStatusTransitionAllowed(ListingStatus currentStatus, ListingStatus nextStatus)
    {
        // Same-value updates are no-op friendly for idempotent client retries.
        if (currentStatus == nextStatus)
        {
            return;
        }

        if (currentStatus == ListingStatus.Archived)
        {
            throw new InvalidListingStatusTransitionException(
                "Archived listings cannot transition to another status.");
        }
    }
}
