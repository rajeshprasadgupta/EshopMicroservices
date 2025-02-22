using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder. HasKey(o => o.Id);
		builder.Property(o => o.Id).HasConversion(
			//so use OrderId.Value when saving to db
			orderId => orderId.Value,
			//and OrderId.Of when reading from db
			dbId => OrderId.Of(dbId));
		builder.HasOne<Customer>()
			//One Customer can have Many Orders
			.WithMany()
			//is referenced through a foreign key CustomerId in Order
			.HasForeignKey(o => o.CustomerId)
			//and is required column in database
			.IsRequired();
		//Order has Many OrderItems
		builder.HasMany(x => x.OrderItems)
			//With Each OrderItem, has one Order
			.WithOne()
			//referenced through a foreign key OrderId
			.HasForeignKey(y => y.OrderId);
		//OrderName is a complex type as it does not have a primary key
		builder.ComplexProperty(o => o.OrderName, nameBuilder =>
		{
			//Identfied by Value property
			nameBuilder.Property(on => on.Value)
			//with maximum length of 100
			.HasMaxLength(100)
			//and is required column in database
			.IsRequired();
		});
		//ShippingAddress is a complex type as it does not have a primary key
		builder.ComplexProperty(o => o.ShippingAddress, addressBuilder =>
		{
			//Has FirstName, LastName, Street, City, State, Country, ZipCode properties
			addressBuilder.Property(a => a.FirstName)
			.HasMaxLength(50)
			.IsRequired();
			addressBuilder.Property(sa => sa.LastName)
			.HasMaxLength(50)
			.IsRequired();
			addressBuilder.Property(sa => sa.EmailAddress)
			.HasMaxLength(250)
			.IsRequired();
			addressBuilder.Property(sa => sa.AddressLine)
			.HasMaxLength(250)
			.IsRequired();
			addressBuilder.Property(sa => sa.Country)
			.HasMaxLength(50)
			.IsRequired();
			addressBuilder.Property(sa => sa.State)
			.HasMaxLength(50)
			.IsRequired();
			addressBuilder.Property(sa => sa.ZipCode)
			.HasMaxLength(10)
			.IsRequired();
		});
		//BillingAddress is a complex type as it does not have a primary key
		builder.ComplexProperty(o => o.BillingAddress, addressBuilder =>
		{
			//Has FirstName, LastName, Street, City, State, Country, ZipCode properties
			addressBuilder.Property(a => a.FirstName)
			.HasMaxLength(50)
			.IsRequired();
			addressBuilder.Property(sa => sa.LastName)
			.HasMaxLength(50)
			.IsRequired();
			addressBuilder.Property(sa => sa.EmailAddress)
			.HasMaxLength(250)
			.IsRequired();
			addressBuilder.Property(sa => sa.AddressLine)
			.HasMaxLength(250)
			.IsRequired();
			addressBuilder.Property(sa => sa.Country)
			.HasMaxLength(50)
			.IsRequired();
			addressBuilder.Property(sa => sa.State)
			.HasMaxLength(50)
			.IsRequired();
			addressBuilder.Property(sa => sa.ZipCode)
			.HasMaxLength(10)
			.IsRequired();
		});
		//Payment is a complex type as it does not have a primary key
		builder.ComplexProperty(o => o.Payment, paymentBuilder =>
		{
			//Has CardNumber, CardHolderName, Expiration, CVV properties
			paymentBuilder.Property(p => p.CardNumber)
			.HasMaxLength(25)
			.IsRequired();
			paymentBuilder.Property(p => p.CardHolderName)
			.HasMaxLength(50)
			.IsRequired();
			paymentBuilder.Property(p => p.Expiration)
			.HasMaxLength(10)
			.IsRequired();
			paymentBuilder.Property(p => p.CVV)
			.HasMaxLength(3)
			.IsRequired();
		});
		builder.Property(o => o.OrderStatus)
			.HasDefaultValue(OrderStatus.Draft)
			.HasConversion(
				s => s.ToString(),
				dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));
		builder.Property(o => o.TotalPrice);
	}
}
