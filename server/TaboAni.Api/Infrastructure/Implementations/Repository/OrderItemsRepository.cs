using Microsoft.EntityFrameworkCore;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Data;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Enums;

namespace TaboAni.Api.Infrastructure.Implementations.Repository;

public sealed class OrderItemsRepository(AppDbContext context) : IOrderItemRepository
{
    private const int SnapshotMaxLength = 150;
    private readonly AppDbContext _context = context;

    public async Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(orderItem);

        await PopulateMissingOrderItemValuesAsync([orderItem], cancellationToken);
        ValidateOrderItem(orderItem);
        await EnsureOrderCanBeModifiedAsync(orderItem.OrderId, cancellationToken);
        await EnsureCreateItemsShareOrderAndFarmerAsync([orderItem], cancellationToken);

        var now = DateTimeOffset.UtcNow;
        orderItem.OrderItemId = Guid.NewGuid();
        orderItem.CreatedAt = now;
        orderItem.UpdatedAt = now;

        await _context.OrderItems.AddAsync(orderItem, cancellationToken);

        return orderItem;
    }

    public async Task<IEnumerable<OrderItem>> CreateBulkOrderItemsAsync(
        IEnumerable<OrderItem> orderItems,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(orderItems);

        var items = orderItems.ToList();
        if (items.Count == 0)
        {
            return items;
        }

        await PopulateMissingOrderItemValuesAsync(items, cancellationToken);

        foreach (var orderItem in items)
        {
            ValidateOrderItem(orderItem);
        }

        await EnsureOrdersCanBeModifiedAsync(items.Select(orderItem => orderItem.OrderId), cancellationToken);
        await EnsureCreateItemsShareOrderAndFarmerAsync(items, cancellationToken);

        var now = DateTimeOffset.UtcNow;
        foreach (var orderItem in items)
        {
            orderItem.OrderItemId = Guid.NewGuid();
            orderItem.CreatedAt = now;
            orderItem.UpdatedAt = now;
        }

        await _context.OrderItems.AddRangeAsync(items, cancellationToken);

        return items;
    }

    public async Task DeleteOrderItemAsync(Guid orderItemId, CancellationToken cancellationToken = default)
    {
        ValidateOrderItemId(orderItemId);

        var orderItem = await _context.OrderItems
            .FirstOrDefaultAsync(existingOrderItem => existingOrderItem.OrderItemId == orderItemId, cancellationToken)
            ?? throw new InvalidOperationException("Order item not found.");

        await EnsureOrderCanBeModifiedAsync(orderItem.OrderId, cancellationToken);

        _context.OrderItems.Remove(orderItem);
    }

    public async Task<OrderItem?> GetOrderItemByIdAsync(Guid orderItemId, CancellationToken cancellationToken = default)
    {
        ValidateOrderItemId(orderItemId);

        return await _context.OrderItems
            .AsNoTracking()
            .Where(orderItem => orderItem.OrderItemId == orderItemId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        ValidateOrderId(orderId);

        return await _context.OrderItems
            .AsNoTracking()
            .Where(orderItem => orderItem.OrderId == orderId)
            .OrderBy(orderItem => orderItem.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(orderItem);
        ValidateOrderItemId(orderItem.OrderItemId);
        ValidateOrderItem(orderItem);

        var existingOrderItem = await _context.OrderItems
            .FirstOrDefaultAsync(currentOrderItem => currentOrderItem.OrderItemId == orderItem.OrderItemId, cancellationToken)
            ?? throw new InvalidOperationException("Order item not found.");

        EnsureImmutableFieldsAreUnchanged(existingOrderItem, orderItem);
        await EnsureOrderCanBeModifiedAsync(existingOrderItem.OrderId, cancellationToken);

        existingOrderItem.QuantityKg = orderItem.QuantityKg;
        existingOrderItem.UnitPricePerKg = orderItem.UnitPricePerKg;
        existingOrderItem.LineSubtotalAmount = orderItem.LineSubtotalAmount;
        existingOrderItem.UpdatedAt = DateTimeOffset.UtcNow;

        return existingOrderItem;
    }

    private async Task PopulateMissingOrderItemValuesAsync(
        IReadOnlyCollection<OrderItem> orderItems,
        CancellationToken cancellationToken)
    {
        var listingIds = orderItems
            .Select(orderItem => orderItem.ProduceListingId)
            .Distinct()
            .ToList();

        if (listingIds.Count == 0)
        {
            return;
        }

        var listings = await _context.ProduceListings
            .AsNoTracking()
            .Where(listing => listingIds.Contains(listing.ProduceListingId))
            .ToDictionaryAsync(listing => listing.ProduceListingId, cancellationToken);

        foreach (var orderItem in orderItems)
        {
            if (!listings.TryGetValue(orderItem.ProduceListingId, out var listing))
            {
                continue;
            }

            if (orderItem.FarmerProfileId == Guid.Empty)
            {
                orderItem.FarmerProfileId = listing.FarmerProfileId;
            }

            if (string.IsNullOrWhiteSpace(orderItem.ListingTitleSnapshot))
            {
                orderItem.ListingTitleSnapshot = listing.ListingTitle;
            }

            if (string.IsNullOrWhiteSpace(orderItem.ProduceNameSnapshot))
            {
                orderItem.ProduceNameSnapshot = listing.ProduceName;
            }

            if (orderItem.UnitPricePerKg <= 0)
            {
                orderItem.UnitPricePerKg = listing.PricePerKg;
            }

            if (orderItem.LineSubtotalAmount <= 0)
            {
                orderItem.LineSubtotalAmount = decimal.Round(
                    orderItem.QuantityKg * orderItem.UnitPricePerKg,
                    2,
                    MidpointRounding.AwayFromZero);
            }
        }
    }

    private async Task EnsureOrderCanBeModifiedAsync(Guid orderId, CancellationToken cancellationToken)
    {
        ValidateOrderId(orderId);

        var order = await _context.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(existingOrder => existingOrder.OrderId == orderId, cancellationToken)
            ?? throw new InvalidOperationException("Cannot modify order item because the associated order does not exist.");

        if (order.OrderStatus != OrderStatus.PendingDownpayment && order.OrderStatus != OrderStatus.PendingFinalPayment)
        {
            throw new InvalidOperationException("Cannot modify order item because the associated order is already completed or cancelled.");
        }
    }

    private async Task EnsureOrdersCanBeModifiedAsync(IEnumerable<Guid> orderIds, CancellationToken cancellationToken)
    {
        var distinctOrderIds = orderIds
            .Distinct()
            .ToList();

        foreach (var orderId in distinctOrderIds)
        {
            ValidateOrderId(orderId);
        }

        var orders = await _context.Orders
            .AsNoTracking()
            .Where(order => distinctOrderIds.Contains(order.OrderId))
            .ToDictionaryAsync(order => order.OrderId, cancellationToken);

        foreach (var orderId in distinctOrderIds)
        {
            if (!orders.TryGetValue(orderId, out var order))
            {
                throw new InvalidOperationException("Cannot modify order item because the associated order does not exist.");
            }

            if (order.OrderStatus != OrderStatus.PendingDownpayment && order.OrderStatus != OrderStatus.PendingFinalPayment)
            {
                throw new InvalidOperationException("Cannot modify order item because the associated order is already completed or cancelled.");
            }
        }
    }

    private async Task EnsureCreateItemsShareOrderAndFarmerAsync(
        IReadOnlyCollection<OrderItem> orderItems,
        CancellationToken cancellationToken)
    {
        var distinctOrderIds = orderItems
            .Select(orderItem => orderItem.OrderId)
            .Distinct()
            .ToList();

        if (distinctOrderIds.Count != 1)
        {
            throw new InvalidOperationException("Order items must all belong to the same order.");
        }

        var distinctFarmerIds = orderItems
            .Select(orderItem => orderItem.FarmerProfileId)
            .Distinct()
            .ToList();

        if (distinctFarmerIds.Count != 1)
        {
            throw new InvalidOperationException("Order items must all belong to the same farmer.");
        }

        var orderId = distinctOrderIds[0];
        var farmerProfileId = distinctFarmerIds[0];

        await EnsureProduceListingsMatchFarmerAsync(orderItems, farmerProfileId, cancellationToken);
        await EnsureExistingOrderItemsUseSameFarmerAsync(orderId, farmerProfileId, cancellationToken);
    }

    private async Task EnsureProduceListingsMatchFarmerAsync(
        IReadOnlyCollection<OrderItem> orderItems,
        Guid farmerProfileId,
        CancellationToken cancellationToken)
    {
        var listingIds = orderItems
            .Select(orderItem => orderItem.ProduceListingId)
            .Distinct()
            .ToList();

        var listings = await _context.ProduceListings
            .AsNoTracking()
            .Where(listing => listingIds.Contains(listing.ProduceListingId))
            .ToDictionaryAsync(listing => listing.ProduceListingId, cancellationToken);

        foreach (var orderItem in orderItems)
        {
            if (!listings.TryGetValue(orderItem.ProduceListingId, out var listing))
            {
                throw new InvalidOperationException("Cannot create order item because the associated produce listing does not exist.");
            }

            if (listing.FarmerProfileId != farmerProfileId)
            {
                throw new InvalidOperationException("Order items must all reference produce listings from the same farmer.");
            }
        }
    }

    private async Task EnsureExistingOrderItemsUseSameFarmerAsync(
        Guid orderId,
        Guid farmerProfileId,
        CancellationToken cancellationToken)
    {
        var existingFarmerIds = await _context.OrderItems
            .AsNoTracking()
            .Where(orderItem => orderItem.OrderId == orderId)
            .Select(orderItem => orderItem.FarmerProfileId)
            .Distinct()
            .ToListAsync(cancellationToken);

        if (existingFarmerIds.Count > 1)
        {
            throw new InvalidOperationException("Existing order items for this order are already inconsistent across farmers.");
        }

        if (existingFarmerIds.Count == 1 && existingFarmerIds[0] != farmerProfileId)
        {
            throw new InvalidOperationException("Order items for the same order must belong to the same farmer.");
        }
    }

    private static void EnsureImmutableFieldsAreUnchanged(OrderItem existingOrderItem, OrderItem updatedOrderItem)
    {
        if (existingOrderItem.OrderId != updatedOrderItem.OrderId)
        {
            throw new InvalidOperationException("Order item cannot be moved to a different order.");
        }

        if (existingOrderItem.ProduceListingId != updatedOrderItem.ProduceListingId)
        {
            throw new InvalidOperationException("Order item produce listing cannot be changed.");
        }

        if (existingOrderItem.FarmerProfileId != updatedOrderItem.FarmerProfileId)
        {
            throw new InvalidOperationException("Order item farmer profile cannot be changed.");
        }

        if (!string.Equals(existingOrderItem.ListingTitleSnapshot, updatedOrderItem.ListingTitleSnapshot, StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Order item listing title snapshot cannot be changed.");
        }

        if (!string.Equals(existingOrderItem.ProduceNameSnapshot, updatedOrderItem.ProduceNameSnapshot, StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Order item produce name snapshot cannot be changed.");
        }
    }

    private static void ValidateOrderItem(OrderItem orderItem)
    {
        ValidateOrderId(orderItem.OrderId);

        if (orderItem.ProduceListingId == Guid.Empty)
        {
            throw new ArgumentException("Produce listing ID is required.", nameof(orderItem.ProduceListingId));
        }

        if (orderItem.FarmerProfileId == Guid.Empty)
        {
            throw new ArgumentException("Farmer profile ID is required.", nameof(orderItem.FarmerProfileId));
        }

        ValidateRequiredSnapshot(orderItem.ListingTitleSnapshot, nameof(orderItem.ListingTitleSnapshot));
        ValidateRequiredSnapshot(orderItem.ProduceNameSnapshot, nameof(orderItem.ProduceNameSnapshot));
        ValidateScale(orderItem.QuantityKg, 3, nameof(orderItem.QuantityKg), "Quantity cannot have more than 3 decimal places.");
        ValidateScale(orderItem.UnitPricePerKg, 2, nameof(orderItem.UnitPricePerKg), "Unit price cannot have more than 2 decimal places.");
        ValidateScale(orderItem.LineSubtotalAmount, 2, nameof(orderItem.LineSubtotalAmount), "Line subtotal cannot have more than 2 decimal places.");

        if (orderItem.QuantityKg <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(orderItem.QuantityKg));
        }

        if (orderItem.UnitPricePerKg <= 0)
        {
            throw new ArgumentException("Unit price must be greater than zero.", nameof(orderItem.UnitPricePerKg));
        }

        var expectedLineSubtotal = decimal.Round(
            orderItem.QuantityKg * orderItem.UnitPricePerKg,
            2,
            MidpointRounding.AwayFromZero);

        if (orderItem.LineSubtotalAmount != expectedLineSubtotal)
        {
            throw new ArgumentException(
                "Line subtotal must equal quantity multiplied by unit price.",
                nameof(orderItem.LineSubtotalAmount));
        }
    }

    private static void ValidateOrderItemId(Guid orderItemId)
    {
        if (orderItemId == Guid.Empty)
        {
            throw new ArgumentException("Order item ID is required.", nameof(orderItemId));
        }
    }

    private static void ValidateOrderId(Guid orderId)
    {
        if (orderId == Guid.Empty)
        {
            throw new ArgumentException("Order ID is required.", nameof(orderId));
        }
    }

    private static void ValidateRequiredSnapshot(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Snapshot value is required.", parameterName);
        }

        if (value.Length > SnapshotMaxLength)
        {
            throw new ArgumentException($"Snapshot value cannot exceed {SnapshotMaxLength} characters.", parameterName);
        }
    }

    private static void ValidateScale(decimal value, int scale, string parameterName, string errorMessage)
    {
        if (decimal.Round(value, scale) != value)
        {
            throw new ArgumentException(errorMessage, parameterName);
        }
    }
}
 
