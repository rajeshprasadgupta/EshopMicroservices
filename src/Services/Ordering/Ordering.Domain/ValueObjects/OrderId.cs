
namespace Ordering.Domain.ValueObjects
{
	public record OrderId
	{
		public Guid Value { get; }
		private OrderId(Guid guid)
		{
			Value = guid;
		}
		public static OrderId Of(Guid guid)
		{
			ArgumentNullException.ThrowIfNull(guid);
			if (guid == Guid.Empty)
			{
				throw new DomainException("OrderId cannot be empty");
			}
			return new OrderId(guid);
		}
	}
}
