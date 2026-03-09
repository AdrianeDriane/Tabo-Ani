namespace TaboAni.Api.Models;

public class ProduceListing
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal PricePerKg { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}