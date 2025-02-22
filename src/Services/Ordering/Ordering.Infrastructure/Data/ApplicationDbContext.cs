
namespace Ordering.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			:base(options)
		{
		}
		public DbSet<Order> Orders => Set<Order>();
		public DbSet<Customer> Customers => Set<Customer>();
		public DbSet<Product> Products => Set<Product>();
		public DbSet<OrderItem> OrderItems => Set<OrderItem>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
			base.OnModelCreating(modelBuilder);
		}
	}
}
