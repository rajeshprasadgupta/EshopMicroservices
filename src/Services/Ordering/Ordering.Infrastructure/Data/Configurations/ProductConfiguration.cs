
namespace Ordering.Infrastructure.Data.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(x => x.Id);
			//Id is of ProductId type
			builder.Property(x => x.Id).HasConversion(
				//so use ProductId.Value when saving to db
				productId => productId.Value,
				//and ProductId.Of when reading from db
				dbId => ProductId.Of(dbId));
			builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
		}
	}
}
