
using Basket.API.Data;

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
		(IBasketRepository repository)
		: ICommandHandler<StoreBasketCommand, StoreBasketResult>
	{
		public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
		{
			var result = await repository.StoreBasket(command.Cart, cancellationToken);
			return new StoreBasketResult(result.UserName);
		}
	}
	
}
