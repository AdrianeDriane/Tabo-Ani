using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class FarmerProfileNotFoundException : DomainException
{
    public FarmerProfileNotFoundException(Guid farmerProfileId)
        : base("farmer_profile_not_found", $"Farmer profile '{farmerProfileId}' was not found.", HttpStatusCode.NotFound)
    {
    }
}
