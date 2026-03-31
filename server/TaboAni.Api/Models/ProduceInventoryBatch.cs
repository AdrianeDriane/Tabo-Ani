namespace TaboAni.Api.Models;

public class ProduceInventoryBatch
{
    public Guid ProduceInventoryBatchId { get; set; }
    public Guid ProduceListingId { get; set; }
    public string? BatchCode { get; set; }
    public DateOnly? EstimatedHarvestDate { get; set; }
    public DateOnly? ActualHarvestDate { get; set; }
    public decimal AvailableQuantityKg { get; set; }
    public decimal ReservedQuantityKg { get; set; }
    public string InventoryStatus { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
