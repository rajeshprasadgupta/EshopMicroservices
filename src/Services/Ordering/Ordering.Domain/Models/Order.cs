
using System;

namespace Ordering.Domain.Models
{
	public class Order : Aggregate<OrderId>
	{
		private readonly List<OrderItem> _orderItems = new();
		public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
		public CustomerId CustomerId { get; private set; } = default!;
		public OrderName OrderName { get; private set; } = default!;
		public Address BillingAddress { get; private set; } = default!;
		public Address ShippingAddress { get; private set; } = default!;
		public Payment Payment { get; private set; } = default!;
		public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;
		public decimal TotalPrice
		{
			get => _orderItems.Sum(x => x.Price * x.Quantity);
			private set { }
		}
		public static Order Create(OrderId Id, CustomerId customerId, OrderName orderName, Address billingAddress, Address shippingAddress, Payment payment)
		{
			var order = new Order
			{
				Id = Id,
				CustomerId = customerId,
				OrderName = orderName,
				BillingAddress = billingAddress,
				ShippingAddress = shippingAddress,
				Payment = payment,
				OrderStatus = OrderStatus.Pending
			};
			order.AddDomainEvent(new OrderCreatedEvent(order));
			return order;
		}

		public void Update( CustomerId customerId, OrderName orderName, Address billingAddress, Address shippingAddress, Payment payment, OrderStatus status)
		{
			CustomerId = customerId;
			OrderName = orderName;
			BillingAddress = billingAddress;
			ShippingAddress = shippingAddress;
			Payment = payment;
			OrderStatus = status;
			AddDomainEvent(new OrderUpdatedEvent(this));
		}
		public void RemoveOrderItem(ProductId productId)
		{
			var item = _orderItems.FirstOrDefault(x => x.ProductId == productId);
			if(item != null)
				_orderItems.Remove(item);
		}

		public void AddOrderItem(ProductId productid, int quantity, decimal price)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
			var item = new OrderItem(Id, productid, price, quantity);
			_orderItems.Add(item);
		}
	}
}
