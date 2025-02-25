using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace BuildingBlocks.Messaging.MassTransit;
public static class Extensions
{
	public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
	{
		services.AddMassTransit(busRegistrationConfigurator =>
		{
			//set naming convention for endpoints
			busRegistrationConfigurator.SetKebabCaseEndpointNameFormatter();
			if(assembly != null)
			{
				//add consumers for the event broker from the calling assembly
				busRegistrationConfigurator.AddConsumers(assembly);
			}
			busRegistrationConfigurator.UsingRabbitMq((busRegistrationContext, rabbbitMqFactoryConfig) =>
			{
				rabbbitMqFactoryConfig.Host(new Uri(configuration["MessageBroker:Host"]!), rabbitMqHostConfig =>
				{
					rabbitMqHostConfig.Username(configuration["MessageBroker:Username"]!);
					rabbitMqHostConfig.Password(configuration["MessageBroker:Password"]!);
				});
				rabbbitMqFactoryConfig.ConfigureEndpoints(busRegistrationContext);
			});
		});
		return services;
	}
}
