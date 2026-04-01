using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.Domain.Validation;

public static class ListingStatusPolicy
{
    public const string Active = "ACTIVE";
    public const string Inactive = "INACTIVE";
    public const string Archived = "ARCHIVED";

    // TODO: Back these status rules with a database constraint once the lifecycle is finalized.
    private static readonly HashSet<string> SupportedStatuses = new(StringComparer.Ordinal)
    {
        Active,
        Inactive,
        Archived
    };

    public static string DefaultCreateStatus => Active;

    public static string Normalize(string? listingStatus)
    {
        if (string.IsNullOrWhiteSpace(listingStatus))
        {
            throw new InvalidListingException("ListingStatus is required.");
        }

        var normalizedStatus = listingStatus.Trim().ToUpperInvariant();

        if (!SupportedStatuses.Contains(normalizedStatus))
        {
            throw new InvalidListingException("ListingStatus must be one of: ACTIVE, INACTIVE, ARCHIVED.");
        }

        return normalizedStatus;
    }

    public static void EnsureTransitionAllowed(string currentStatus, string nextStatus)
    {
        var normalizedCurrentStatus = Normalize(currentStatus);
        var normalizedNextStatus = Normalize(nextStatus);

        // Same-value updates are treated as no-op friendly requests for simpler client retries.
        if (normalizedCurrentStatus == normalizedNextStatus)
        {
            return;
        }

        if (normalizedCurrentStatus == Archived)
        {
            throw new InvalidListingStatusTransitionException(
                "Archived listings cannot transition to another status.");
        }
    }
}
