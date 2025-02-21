
namespace Ordering.Domain.ValueObjects
{
	public record ProductId
	{
		public Guid Value { get; }

		private ProductId(Guid guid)
		{
			Value = guid;
		}
		public static ProductId Of(Guid guid)
		{
			ArgumentNullException.ThrowIfNull(guid);
			if (guid == Guid.Empty)
			{
				throw new DomainException("ProductId cannot be empty");
			}
			return new ProductId(guid);
		}
	}
}
