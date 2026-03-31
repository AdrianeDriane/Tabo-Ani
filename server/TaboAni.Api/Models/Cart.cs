namespace TaboAni.Api.Models;

public class Cart
{
    public Guid CartId { get; set; }
    public Guid UserId { get; set; }
    public string CartStatus { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
