using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace Ordering.Infrastructure.Data.Extensions
{
	public static class DatabaseExtensions
	{
		public static async Task InitializeDatabaseAsync(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();
			try
			{
				var context = services.GetRequiredService<ApplicationDbContext>();
				await context.Database.MigrateAsync();
				await context.SeedAsync();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while migrating or seeding the database.");
			}
		}

		private static async Task SeedAsync(this ApplicationDbContext context)
		{
			await context.SeedCustomersAsync();
			await context.SeedProductsAsync();
			await context.SeedOrderAndItemsAsync();
		}

		private static async Task SeedOrderAndItemsAsync(this ApplicationDbContext context)
		{
			if(context.Orders.Any())
			{
				return;
			}
			await context.Orders.AddRangeAsync(InitialSeedData.GetOrders());
			await context.SaveChangesAsync();
		}

		private static async Task SeedProductsAsync(this ApplicationDbContext context)
		{
			if (context.Products.Any())
			{
				return;
			}
			await context.Products.AddRangeAsync(InitialSeedData.GetProducts());
			await context.SaveChangesAsync();
		}

		private static async Task SeedCustomersAsync(this ApplicationDbContext context)
		{
			if(context.Customers.Any())
			{
				return;
			}
			await context.Customers.AddRangeAsync(InitialSeedData.GetCustomers());
			await context.SaveChangesAsync();
		}
	}
}