namespace Ordering.Infrastructure.Data.Configurations
{
	public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.HasKey(x => x.Id);
			//Id is of CustomerId type
			builder.Property(x => x.Id).HasConversion(
				//so use CustomerId.Value when saving to db
				customerId => customerId.Value,
				//and CustomerId.Of when reading from db
				dbId => CustomerId.Of(dbId));
			builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
			builder.Property(x => x.Email).HasMaxLength(250);
			builder.HasIndex(x => x.Email).IsUnique();
		}
	}
}
