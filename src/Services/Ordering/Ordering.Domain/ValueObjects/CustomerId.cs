
namespace Ordering.Domain.ValueObjects
{
	public record CustomerId
	{
		public Guid Value { get; }

		private CustomerId(Guid value)
		{
			Value = value;
		}
		public static CustomerId Of(Guid guid)
		{
			ArgumentNullException.ThrowIfNull(guid);
			if(guid == Guid.Empty)
			{
				throw new DomainException("CustomerId cannot be empty");
			}
			return new CustomerId(value: guid);
		}
	}
}
