
using Basket.API.Data;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
	public CheckoutBasketCommandValidator()
	{
		RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto can't be null");
		RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
	}
}
public class CheckoutBasketCommandHandler
	(IBasketRepository repository, IPublishEndpoint publishEndpoint)
	: ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
	public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
	{
		//get existing basket with total price
		var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
		if (basket == null)
		{
			return new CheckoutBasketResult(false);
		}
		//create basketCheckout event -- set total price on basketCheckout event message
		var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
		eventMessage.TotalPrice = basket.TotalPrice;
		//send checkout event to rabbitmq
		await publishEndpoint.Publish(eventMessage, cancellationToken);
		//remove the basket
		await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);
		return new CheckoutBasketResult(true);
	}
}

