
using Basket.API.Data;
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
	public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
	public record StoreBasketResult(string UserName);

	public class StoreCommandValidator
		: AbstractValidator<StoreBasketCommand>
	{
		public StoreCommandValidator()
		{
			RuleFor(x => x.Cart).NotNull();
			RuleFor(x => x.Cart.UserName).NotEmpty();
		}
	}
	public class StoreBasketCommandHandler
		(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountservice)
		: ICommandHandler<StoreBasketCommand, StoreBasketResult>
	{
		public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
		{
			//Get Discount from Discount service and apply discount and update the price in the cart
			await DeductDiscountAsync(command.Cart, cancellationToken);
			var result = await repository.StoreBasket(command.Cart, cancellationToken);
			return new StoreBasketResult(result.UserName);
		}

		private async Task DeductDiscountAsync( ShoppingCart cart, CancellationToken token)
		{
			foreach (var item in cart.Items)
			{
				var coupon = await discountservice.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });
				item.Price -= coupon.Amount;
			}
		}
	}
	
}
