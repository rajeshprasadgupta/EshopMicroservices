using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructreServices(this IServiceCollection services, 
			IConfiguration  configuration)
		{
			services.AddScoped<ISaveChangesInterceptor,AuditableEntityInterceptor>();
			services.AddScoped<ISaveChangesInterceptor,DispatchDomainEventsInterceptor>();
			var connectionString = configuration.GetConnectionString("Database");
			services.AddDbContext<ApplicationDbContext>((serviceProvider,options) =>{
				options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
				options.UseSqlServer(connectionString);
			});
			services.AddScoped<IApplicationDbContext,ApplicationDbContext>();

			return services;
		}
	}
}
