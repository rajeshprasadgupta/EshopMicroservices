
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
	public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache, ILogger<CachedBasketRepository> logger) : IBasketRepository
	{
		public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
		{
			await repository.DeleteBasket(userName, cancellationToken);
			await cache.RemoveAsync(userName, cancellationToken);
			return true;
		}

		public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
		{
			//fetch from cache
			 var cachedBasketString = await cache.GetStringAsync(userName, cancellationToken);
			if (!string.IsNullOrEmpty(cachedBasketString))
			{
				var cachedBasket = JsonSerializer.Deserialize<ShoppingCart>(cachedBasketString);
				logger.LogInformation("Basket {username} fetched from cache", userName);
				return cachedBasket!;
			}
			var basket = await repository.GetBasket(userName, cancellationToken);
			await cache.SetStringAsync(userName, JsonSerializer.Serialize<ShoppingCart>(basket), cancellationToken);
			return basket;
		}

		public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken)
		{
			basket = await repository.StoreBasket(basket, cancellationToken);
			await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize<ShoppingCart>(basket), cancellationToken);
			return basket;
		}
	}
}
