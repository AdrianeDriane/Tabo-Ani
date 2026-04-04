using TaboAni.Api.Domain.Exceptions;
using TaboAni.Api.Domain.Enums;

namespace TaboAni.Api.Domain.Policy;

public static class InventoryStatusPolicy
{
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
            throw new InvalidInventoryBatchException("ReservedQuantityKg cannot exceed AvailableQuantityKg.");
        }

        if (estimatedHarvestDate.HasValue &&
            actualHarvestDate.HasValue &&
            actualHarvestDate.Value < estimatedHarvestDate.Value)
        {
            throw new InvalidInventoryBatchException(
                "ActualHarvestDate cannot be earlier than EstimatedHarvestDate.");
        }
    }

    public static InventoryStatus DeriveStatus(
        decimal availableQuantityKg,
        decimal reservedQuantityKg,
        DateOnly? estimatedHarvestDate,
        DateOnly? actualHarvestDate)
    {
        _ = estimatedHarvestDate;
        _ = actualHarvestDate;

        if (availableQuantityKg <= 0)
        {
            return InventoryStatus.Depleted;
        }

        if (reservedQuantityKg <= 0)
        {
            return InventoryStatus.Available;
        }

        if (reservedQuantityKg >= availableQuantityKg)
        {
            return InventoryStatus.FullyReserved;
        }

        return InventoryStatus.PartiallyReserved;
    }
}
