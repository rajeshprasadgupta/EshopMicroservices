﻿
namespace Ordering.Application.Orders.Commands.UpdateOrder;
public class UpdateOrderHandler
	(IApplicationDbContext dbContext)
	: ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
	public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
	{
		var orderId = OrderId.Of(command.Order.Id);
		var order = await dbContext.Orders.FindAsync([orderId] , cancellationToken);
		if (order == null)
		{
			throw new OrderNotFoundException(orderId.Value);
		}
		UpdateOrderWithNewValues(order, command.Order);
		dbContext.Orders.Update(order);
		await dbContext.SaveChangesAsync(cancellationToken);
		return new UpdateOrderResult(true);
	}

	private void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
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
		order.Update(CustomerId.Of(orderDto.CustomerId), 
			OrderName.Of(orderDto.OrderName), 
			billingAddress, 
			shippingAddress, 
			payment, 
			orderDto.Status);
	}
}
