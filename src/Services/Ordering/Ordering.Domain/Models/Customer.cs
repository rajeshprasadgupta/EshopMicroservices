
namespace Ordering.Domain.Models
{
	public class Customer :Entity<CustomerId>
	{
		public string Name { get; private set; } = default;
		public string Email { get; private set; } = default;
		public static Customer Create(CustomerId Id, string name, string email)
		{
			ArgumentException.ThrowIfNullOrEmpty(name);
			ArgumentException.ThrowIfNullOrEmpty(email);
			return new Customer
			{
				Id = Id,
				Name = name,
				Email = email
			};
		}
	}
	
}
