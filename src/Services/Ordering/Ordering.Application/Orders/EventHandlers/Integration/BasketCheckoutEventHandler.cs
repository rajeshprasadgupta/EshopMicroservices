using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;
public class BasketCheckoutEventHandler
	(ISender sender, ILogger<BasketCheckoutEventHandler> logger)
	: IConsumer<BasketCheckoutEvent>
{
	public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
	{
		logger.LogInformation("Integration Event handlers: {IntegrationEvent}", context.GetType().Name);
		//create new order and start order fulfillment process
		var command = MapToCreateOrderCommand(context.Message);
		await sender.Send(command);
	}

	private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
	{
		var addressDto = new AddressDto(message.FirstName, message.LastName,message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
		var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
		var orderId = Guid.NewGuid();
		var orderDto = new OrderDto(
			Id: orderId,
			CustomerId: message.CustomerId,
			OrderName: message.UserName,
			ShippingAddress: addressDto,
			BillingAddress: addressDto,
			Payment: paymentDto,
			Status: Ordering.Domain.Enums.OrderStatus.Pending,
			OrderItems:
			[
				new OrderItemDto(orderId, new Guid("01950f0f-9591-4535-927b-bf084054f476"), 2, 100),
				new OrderItemDto(orderId, new Guid("01950f0f-9594-43c3-beb8-df7fcd377b8d"), 1, 200)
			]);
		return new CreateOrderCommand(orderDto);
	}
}
