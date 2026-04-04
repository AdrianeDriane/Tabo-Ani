using Microsoft.EntityFrameworkCore;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Data;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Enums;

namespace TaboAni.Api.Infrastructure.Implementations.Repository;

public sealed class CartRepository(AppDbContext context) : ICartRepository
{
    private readonly AppDbContext _context = context;

    public Task<bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return _context.Users
            .AsNoTracking()
            .AnyAsync(user => user.UserId == userId, cancellationToken);
    }

    public Task<bool> IsFarmerProfileOwnedByUserAsync(
        Guid farmerProfileId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return _context.FarmerProfiles
            .AsNoTracking()
            .AnyAsync(profile => profile.FarmerProfileId == farmerProfileId && profile.UserId == userId, cancellationToken);
    }

    public Task<Cart?> GetCartByUserIdForUpdateAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return _context.Carts
            .SingleOrDefaultAsync(cart => cart.UserId == userId, cancellationToken);
    }

    public Task AddCartAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        return _context.Carts.AddAsync(cart, cancellationToken).AsTask();
    }

    public Task<CartItem?> GetCartItemByIdForUpdateAsync(Guid cartItemId, CancellationToken cancellationToken = default)
    {
        return _context.CartItems
            .SingleOrDefaultAsync(cartItem => cartItem.CartItemId == cartItemId, cancellationToken);
    }

    public Task<CartItem?> GetCartItemByCartIdAndListingIdForUpdateAsync(
        Guid cartId,
        Guid produceListingId,
        CancellationToken cancellationToken = default)
    {
        return _context.CartItems
            .SingleOrDefaultAsync(
                cartItem => cartItem.CartId == cartId && cartItem.ProduceListingId == produceListingId,
                cancellationToken);
    }

    public Task AddCartItemAsync(CartItem cartItem, CancellationToken cancellationToken = default)
    {
        return _context.CartItems.AddAsync(cartItem, cancellationToken).AsTask();
    }

    public void RemoveCartItem(CartItem cartItem)
    {
        _context.CartItems.Remove(cartItem);
    }

    public Task<CartListingSnapshotDto?> GetListingSnapshotAsync(
        Guid produceListingId,
        CancellationToken cancellationToken = default)
    {
        return _context.ProduceListings
            .AsNoTracking()
            .Where(listing => listing.ProduceListingId == produceListingId)
            .Select(listing => new CartListingSnapshotDto(
                listing.ProduceListingId,
                listing.FarmerProfileId,
                _context.FarmerProfiles
                    .Where(profile => profile.FarmerProfileId == listing.FarmerProfileId)
                    .Select(profile => profile.FarmName)
                    .FirstOrDefault() ?? string.Empty,
                listing.ListingTitle,
                listing.ProduceName,
                listing.PricePerKg,
                listing.MinimumOrderKg,
                listing.MaximumOrderKg,
                listing.ListingStatus,
                listing.PrimaryLocationText,
                _context.ProduceListingImages
                    .Where(image => image.ProduceListingId == listing.ProduceListingId)
                    .OrderByDescending(image => image.IsPrimary)
                    .ThenBy(image => image.DisplayOrder)
                    .Select(image => image.ImageUrl)
                    .FirstOrDefault()))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<ActiveCartQueryResultDto?> GetActiveCartAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var cart = await _context.Carts
            .AsNoTracking()
            .Where(existingCart => existingCart.UserId == userId && existingCart.CartStatus == CartStatus.Active)
            .Select(existingCart => new
            {
                existingCart.CartId,
                existingCart.UserId,
                existingCart.CartStatus
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (cart is null)
        {
            return null;
        }

        // Keep active-cart reads bounded to the cart header and a single projected item query.
        var items = await _context.CartItems
            .AsNoTracking()
            .Where(cartItem => cartItem.CartId == cart.CartId)
            .OrderBy(cartItem => cartItem.CreatedAt)
            .Select(cartItem => new CartItemQueryResultDto(
                cartItem.CartItemId,
                cartItem.ProduceListingId,
                _context.ProduceListings
                    .Where(listing => listing.ProduceListingId == cartItem.ProduceListingId)
                    .Select(listing => listing.FarmerProfileId)
                    .FirstOrDefault(),
                _context.ProduceListings
                    .Where(listing => listing.ProduceListingId == cartItem.ProduceListingId)
                    .Select(listing => _context.FarmerProfiles
                        .Where(profile => profile.FarmerProfileId == listing.FarmerProfileId)
                        .Select(profile => profile.FarmName)
                        .FirstOrDefault() ?? string.Empty)
                    .FirstOrDefault() ?? string.Empty,
                _context.ProduceListings
                    .Where(listing => listing.ProduceListingId == cartItem.ProduceListingId)
                    .Select(listing => listing.ListingTitle)
                    .FirstOrDefault() ?? string.Empty,
                _context.ProduceListings
                    .Where(listing => listing.ProduceListingId == cartItem.ProduceListingId)
                    .Select(listing => listing.ProduceName)
                    .FirstOrDefault() ?? string.Empty,
                _context.ProduceListings
                    .Where(listing => listing.ProduceListingId == cartItem.ProduceListingId)
                    .Select(listing => listing.PricePerKg)
                    .FirstOrDefault(),
                cartItem.QuantityKg,
                _context.ProduceListings
                    .Where(listing => listing.ProduceListingId == cartItem.ProduceListingId)
                    .Select(listing => listing.MinimumOrderKg)
                    .FirstOrDefault(),
                _context.ProduceListings
                    .Where(listing => listing.ProduceListingId == cartItem.ProduceListingId)
                    .Select(listing => listing.MaximumOrderKg)
                    .FirstOrDefault(),
                _context.ProduceListings
                    .Where(listing => listing.ProduceListingId == cartItem.ProduceListingId)
                    .Select(listing => listing.PrimaryLocationText)
                    .FirstOrDefault() ?? string.Empty,
                _context.ProduceListingImages
                    .Where(image => image.ProduceListingId == cartItem.ProduceListingId)
                    .OrderByDescending(image => image.IsPrimary)
                    .ThenBy(image => image.DisplayOrder)
                    .Select(image => image.ImageUrl)
                    .FirstOrDefault()))
            .ToListAsync(cancellationToken);

        return new ActiveCartQueryResultDto(cart.CartId, cart.UserId, cart.CartStatus, items);
    }
}
