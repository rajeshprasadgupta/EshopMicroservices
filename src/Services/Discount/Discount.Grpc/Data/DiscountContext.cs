using Discount.Grpc.Model;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
	public class DiscountContext : DbContext
	{
		public DiscountContext(DbContextOptions<DiscountContext> options) 
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Coupon>().HasData(
				new Coupon { Id = 1, ProductName = "Product 1", Description = "Product 1 Description", Amount = 10 },
				new Coupon { Id = 2, ProductName = "Product 2", Description = "Product 2 Description", Amount = 20 }
				);
		}
		public DbSet<Coupon> Coupons { get; set; } = default!;
	}
}
