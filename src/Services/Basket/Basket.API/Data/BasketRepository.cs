
namespace Basket.API.Data
{
	public class BasketRepository
		(IDocumentSession session)
		: IBasketRepository
	{
		public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
		{
			var basket = await session.Query<ShoppingCart>().FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);
			if(basket == null)
			{
				throw new BasketNotFoundException(userName);
			}
			session.Delete<ShoppingCart>(userName);
			await session.SaveChangesAsync(cancellationToken);
			return true;
		}

		public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
		{
			var result = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
			if (result == null)
			{
				throw new BasketNotFoundException(userName);
			}
			return result;
		}

		public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken)
		{
			session.Store<ShoppingCart>(basket);
			await session.SaveChangesAsync(cancellationToken);
			return basket;
		}
	}
}
