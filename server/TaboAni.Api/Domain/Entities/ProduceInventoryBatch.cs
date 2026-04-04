using TaboAni.Api.Domain.Exceptions;
using TaboAni.Api.Domain.Validation;

namespace TaboAni.Api.Domain.Entities;

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

    public static ProduceInventoryBatch Create(
        Guid produceListingId,
        string? batchCode,
        DateOnly? estimatedHarvestDate,
        DateOnly? actualHarvestDate,
        decimal availableQuantityKg,
        decimal reservedQuantityKg,
        string? notes,
        DateTimeOffset now)
    {
        if (produceListingId == Guid.Empty)
        {
            throw new InvalidInventoryBatchException("ProduceListingId is required.");
        }

        InventoryStatusPolicy.EnsureValidState(
            availableQuantityKg,
            reservedQuantityKg,
            estimatedHarvestDate,
            actualHarvestDate);

        return new ProduceInventoryBatch
        {
            ProduceInventoryBatchId = Guid.NewGuid(),
            ProduceListingId = produceListingId,
            BatchCode = NormalizeOptionalText(batchCode),
            EstimatedHarvestDate = estimatedHarvestDate,
            ActualHarvestDate = actualHarvestDate,
            AvailableQuantityKg = availableQuantityKg,
            ReservedQuantityKg = reservedQuantityKg,
            InventoryStatus = InventoryStatusPolicy.DeriveStatus(
                availableQuantityKg,
                reservedQuantityKg,
                estimatedHarvestDate,
                actualHarvestDate),
            Notes = NormalizeOptionalText(notes),
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    public void UpdateBatch(
        string? batchCode,
        DateOnly? estimatedHarvestDate,
        DateOnly? actualHarvestDate,
        decimal availableQuantityKg,
        decimal reservedQuantityKg,
        string? notes,
        DateTimeOffset updatedAt)
    {
        InventoryStatusPolicy.EnsureValidState(
            availableQuantityKg,
            reservedQuantityKg,
            estimatedHarvestDate,
            actualHarvestDate);

        BatchCode = NormalizeOptionalText(batchCode);
        EstimatedHarvestDate = estimatedHarvestDate;
        ActualHarvestDate = actualHarvestDate;
        AvailableQuantityKg = availableQuantityKg;
        ReservedQuantityKg = reservedQuantityKg;
        InventoryStatus = InventoryStatusPolicy.DeriveStatus(
            availableQuantityKg,
            reservedQuantityKg,
            estimatedHarvestDate,
            actualHarvestDate);
        Notes = NormalizeOptionalText(notes);
        UpdatedAt = updatedAt;
    }

    private static string? NormalizeOptionalText(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
