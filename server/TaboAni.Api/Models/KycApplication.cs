namespace TaboAni.Api.Models;

public class KycApplication
{
    public Guid KycApplicationId { get; set; }
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public string ApplicationStatus { get; set; } = string.Empty;
    public DateTimeOffset SubmittedAt { get; set; }
    public DateTimeOffset? ReviewedAt { get; set; }
    public string? FinalRemarks { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
