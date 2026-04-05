using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class FarmerProfileOwnershipException(Guid farmerProfileId, Guid authenticatedUserId)
    : DomainException(
        "farmer_profile.ownership_violation",
        $"Farmer profile '{farmerProfileId}' does not belong to authenticated user '{authenticatedUserId}'.",
        HttpStatusCode.Forbidden);
