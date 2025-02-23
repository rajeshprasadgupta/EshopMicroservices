namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler
	(IApplicationDbContext context)
	: ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
	public async Task<CreateOrderResult> Handle(CreateOrderCommand request,
		CancellationToken cancellationToken)
	{
		var order = CreateOrderFromDto(request.Order);
		await context.Orders.AddAsync(order, cancellationToken);
		var result = await context.SaveChangesAsync(cancellationToken);
		return new CreateOrderResult(order.Id.Value);
	}

	private Order CreateOrderFromDto(OrderDto orderDto)
	{
		var billingAddress = Address.Of(orderDto.BillingAddress.FirstName,
			orderDto.BillingAddress.LastName,
			orderDto.BillingAddress.EmailAddress,
			orderDto.BillingAddress.AddressLine,
			orderDto.BillingAddress.Country,
			orderDto.BillingAddress.State,
			orderDto.BillingAddress.ZipCode);
		var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName,
			orderDto.ShippingAddress.LastName,
			orderDto.ShippingAddress.EmailAddress,
			orderDto.ShippingAddress.AddressLine,
			orderDto.ShippingAddress.Country,
			orderDto.ShippingAddress.State,
			orderDto.ShippingAddress.ZipCode);
		var payment = Payment.Of(orderDto.Payment.CardNumber,
			orderDto.Payment.CardName,
			orderDto.Payment.Expiration,
			orderDto.Payment.Cvv,
			orderDto.Payment.PaymentMethod);
		var order = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(orderDto.CustomerId),
			OrderName.Of(orderDto.OrderName), billingAddress, shippingAddress, payment);
		foreach (var item in orderDto.OrderItems)
		{
			order.AddOrderItem(ProductId.Of(item.ProductId), item.Quantity, item.Price);
		}
		return order;
	}
}

