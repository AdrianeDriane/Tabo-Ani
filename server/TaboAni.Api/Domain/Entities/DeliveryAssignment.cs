namespace TaboAni.Api.Domain.Entities;

public class DeliveryAssignment
{
    public Guid DeliveryAssignmentId { get; set; }
    public Guid DeliveryId { get; set; }
    public Guid DistributorUserId { get; set; }
    public string AssignmentStatus { get; set; } = string.Empty;
    public DateTimeOffset AssignedAt { get; set; }
    public DateTimeOffset? EndedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

