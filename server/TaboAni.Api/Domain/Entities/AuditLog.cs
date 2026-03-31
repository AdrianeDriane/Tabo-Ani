using System.Net;
using System.Text.Json;

namespace TaboAni.Api.Domain.Entities;

public class AuditLog
{
    public Guid AuditLogId { get; set; }
    public Guid? ActorUserId { get; set; }
    public string EntityType { get; set; } = string.Empty;
    public Guid? EntityId { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public string ActionSummary { get; set; } = string.Empty;
    public JsonDocument? Metadata { get; set; }
    public IPAddress? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
