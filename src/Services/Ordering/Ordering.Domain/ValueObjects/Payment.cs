
namespace Ordering.Domain.ValueObjects
{
	public record Payment
	{
		public string CardNumber { get; set; } = default!;
		public string CardHolderName { get; set; } = default!;
		public string Expiration { get; set; } = default!;
		public string CVV { get; set; } = default!;
		public int PaymentMethod { get; set; } = default!;

		protected Payment()
		{
		}

		private Payment(string cardNumber, string cardHolderName, string expiration, string cvv, int paymentMethod)
		{
			CardNumber = cardNumber;
			CardHolderName = cardHolderName;
			Expiration = expiration;
			CVV = cvv;
			PaymentMethod = paymentMethod;
		}

		public static Payment Of(string cardNumber, string cardHolderName, string expiration, string cvv, int paymentMethod)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
			ArgumentException.ThrowIfNullOrWhiteSpace(cardHolderName);
			ArgumentException.ThrowIfNullOrWhiteSpace(expiration);
			ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
			ArgumentOutOfRangeException.ThrowIfNotEqual(cvv.Length, 3);
			return new Payment(cardNumber, cardHolderName, expiration, cvv, paymentMethod);
		}

	}
}
