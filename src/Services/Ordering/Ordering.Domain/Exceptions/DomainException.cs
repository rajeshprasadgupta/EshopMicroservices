
namespace Ordering.Domain.Exceptions
{
	public class DomainException : Exception
	{
		public DomainException(string message)
			: base($"Ordering.Domain Exception: {message}")
		{
		}
	}
}
