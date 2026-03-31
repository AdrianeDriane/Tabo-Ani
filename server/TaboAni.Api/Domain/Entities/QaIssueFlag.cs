namespace TaboAni.Api.Domain.Entities;

public class QaIssueFlag
{
    public Guid QaIssueFlagId { get; set; }
    public Guid QaReportId { get; set; }
    public string IssueType { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
}

