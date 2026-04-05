namespace TaboAni.Api.Domain.Entities;

public class UserPolicyAcceptance
{
    public Guid UserPolicyAcceptanceId { get; set; }
    public Guid UserId { get; set; }
    public string PolicyType { get; set; } = string.Empty;
    public string PolicyVersion { get; set; } = string.Empty;
    public DateTimeOffset AcceptedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
