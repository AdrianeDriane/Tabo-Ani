namespace TaboAni.Api.Domain.Entities;

public class Conversation
{
    public Guid ConversationId { get; set; }
    public Guid OrderId { get; set; }
    public string ConversationStatus { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

