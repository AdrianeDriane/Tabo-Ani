using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.Domain.Validation;

public static class InventoryStatusPolicy
{
    public const string Planned = "PLANNED";
    public const string Available = "AVAILABLE";
    public const string Reserved = "RESERVED";
    public const string Harvested = "HARVESTED";
    public const string Depleted = "DEPLETED";

    // TODO: Back inventory statuses with a database constraint once the lifecycle is finalized.
    public static string DeriveStatus(
        decimal availableQuantityKg,
        decimal reservedQuantityKg,
        DateOnly? estimatedHarvestDate,
        DateOnly? actualHarvestDate)
    {
        EnsureValidState(availableQuantityKg, reservedQuantityKg, estimatedHarvestDate, actualHarvestDate);

        if (availableQuantityKg == 0)
        {
            return Depleted;
        }

        if (actualHarvestDate.HasValue)
        {
            return Harvested;
        }

        if (reservedQuantityKg > 0)
        {
            return Reserved;
        }

        if (estimatedHarvestDate.HasValue)
        {
            return Planned;
        }

        return Available;
    }

    public static void EnsureValidState(
        decimal availableQuantityKg,
        decimal reservedQuantityKg,
        DateOnly? estimatedHarvestDate,
        DateOnly? actualHarvestDate)
    {
        if (availableQuantityKg < 0)
        {
            throw new InvalidInventoryBatchException("AvailableQuantityKg cannot be negative.");
        }

        if (reservedQuantityKg < 0)
        {
            throw new InvalidInventoryBatchException("ReservedQuantityKg cannot be negative.");
        }

        if (reservedQuantityKg > availableQuantityKg)
        {
            throw new InvalidInventoryBatchException(
                "ReservedQuantityKg must be less than or equal to AvailableQuantityKg.");
        }

        if (estimatedHarvestDate.HasValue && actualHarvestDate.HasValue &&
            actualHarvestDate.Value < estimatedHarvestDate.Value)
        {
            throw new InvalidInventoryBatchException(
                "ActualHarvestDate must be on or after EstimatedHarvestDate.");
        }
    }
}
