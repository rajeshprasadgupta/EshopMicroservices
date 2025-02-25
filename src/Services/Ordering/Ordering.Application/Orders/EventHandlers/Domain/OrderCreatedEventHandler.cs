using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;
public class OrderCreatedEventHandler
	(IPublishEndpoint publishEndpoint,ILogger<OrderCreatedEventHandler> logger, IFeatureManager featureManager)
	: INotificationHandler<OrderCreatedEvent>
{
	public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
	{
		logger.LogInformation("Domain Event Handled : {DomainEvent}", domainEvent.GetType().Name);
		if (await featureManager.IsEnabledAsync("OrderFullfilment"))
		{
			var orderIntegarionEvent = domainEvent.Order.ToOrderDto();
			await publishEndpoint.Publish(orderIntegarionEvent, cancellationToken);
		}
	}
}
