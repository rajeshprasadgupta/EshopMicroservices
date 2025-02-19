using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
	public static class Extensions
	{
		/// <summary>
		/// Create Database, Run Migrations and seed data
		/// </summary>
		/// <param name="app"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
		{
			using var scope = app.ApplicationServices.CreateScope();
			using var context = scope.ServiceProvider.GetRequiredService<DiscountContext>();
			context.Database.MigrateAsync();
			return app;
		}
	}
}
