namespace Ordering.Infrastructure.Data.Configurations
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.HasKey(x => x.Id);
			//Id is of OrderItemId type
			builder.Property(oi=> oi.Id).HasConversion(
					//so use OrderItemId.Value when saving to db
					orderItemId => orderItemId.Value,
					//and OrderItemId.Of when reading from db
					dbId => OrderItemId.Of(dbId));
			//Each OrderItem has one Product
			builder.HasOne<Product>()
				//One Product can be With Many OrderItems
				.WithMany()
				//is referenced through a foreign key ProductId in OrderItem
				.HasForeignKey(oi => oi.ProductId);
			builder.Property(oi => oi.Quantity).IsRequired();
			builder.Property(oi => oi.Price).IsRequired();
		}
	}
}
