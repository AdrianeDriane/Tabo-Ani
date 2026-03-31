namespace TaboAni.Api.Models;

public class ProduceCategory
{
    public Guid ProduceCategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
