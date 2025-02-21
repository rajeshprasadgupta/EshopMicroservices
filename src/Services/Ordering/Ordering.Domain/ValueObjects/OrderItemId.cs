
namespace Ordering.Domain.ValueObjects
{
	public record OrderItemId
	{
		public Guid Value { get; }

		private OrderItemId(Guid guid)
		{
			Value = guid;
		}
		public static OrderItemId Of(Guid guid)
		{
			ArgumentNullException.ThrowIfNull(guid);
			if (guid == Guid.Empty)
			{
				throw new DomainException("OrderItemId cannot be empty");
			}
			return new OrderItemId(guid);
		}
	}
}
