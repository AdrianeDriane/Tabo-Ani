namespace TaboAni.Api.Domain.Entities;

public class ConversationParticipant
{
    public Guid ConversationParticipantId { get; set; }
    public Guid ConversationId { get; set; }
    public Guid UserId { get; set; }
    public string ParticipantRoleCode { get; set; } = string.Empty;
    public DateTimeOffset JoinedAt { get; set; }
    public DateTimeOffset? LeftAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

