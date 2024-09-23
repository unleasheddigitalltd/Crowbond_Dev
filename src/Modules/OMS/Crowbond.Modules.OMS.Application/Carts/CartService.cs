using Crowbond.Common.Application.Caching;

namespace Crowbond.Modules.OMS.Application.Carts;

public sealed class CartService(ICacheService cacheService)
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(20);

    public async Task<Cart> GetAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        string cacheKey = CreateCacheKey(customerId);

        Cart cart = await cacheService.GetAsync<Cart>(cacheKey, cancellationToken) ??
                    Cart.CreateDefault(customerId);

        return cart;
    }

    public async Task ClearAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        string cacheKey = CreateCacheKey(customerId);

        var cart = Cart.CreateDefault(customerId);

        await cacheService.SetAsync(cacheKey, cart, DefaultExpiration, cancellationToken);
    }

    public async Task AddItemAsync(Guid customerId, CartItem cartItem, CancellationToken cancellationToken = default)
    {
        string cacheKey = CreateCacheKey(customerId);

        Cart cart = await GetAsync(customerId, cancellationToken);

        CartItem? existingCartItem = cart.Items.Find(c => c.ProductId == cartItem.ProductId);

        if (existingCartItem is null)
        {
            cart.Items.Add(cartItem);
        }
        else
        {
            existingCartItem.Qty += cartItem.Qty;
        }

        await cacheService.SetAsync(cacheKey, cart, DefaultExpiration, cancellationToken);
    }

    public async Task RemoveItemAsync(Guid customerId, Guid productId, decimal qty, CancellationToken cancellationToken = default)
    {
        string cacheKey = CreateCacheKey(customerId);

        Cart cart = await GetAsync(customerId, cancellationToken);

        CartItem? cartItem = cart.Items.Find(c => c.ProductId == productId);

        if (cartItem is null)
        {
            return;
        }

        if (cartItem.Qty < qty)
        {
            return;
        }

        if (cartItem.Qty == qty)
        {
            cart.Items.Remove(cartItem);
        }
        else
        {
            cartItem.Qty -= qty;
        }

        await cacheService.SetAsync(cacheKey, cart, DefaultExpiration, cancellationToken);
    }

    private static string CreateCacheKey(Guid customerId) => $"carts:{customerId}";
}
