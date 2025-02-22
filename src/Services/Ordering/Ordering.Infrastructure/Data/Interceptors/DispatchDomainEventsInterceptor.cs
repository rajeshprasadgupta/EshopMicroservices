

using MediatR;

namespace Ordering.Infrastructure.Data.Interceptors
{
	public class DispatchDomainEventsInterceptor
		( IMediator mediator)
		: SaveChangesInterceptor
	{
		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
			return base.SavingChanges(eventData, result);
		}

		public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			await DispatchDomainEvents(eventData.Context);
			return await base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		private async Task DispatchDomainEvents(DbContext? dbContext) {
			if (dbContext == null) return;
			//select aggregates that include any Domain Events
			var aggregates = dbContext.ChangeTracker
				.Entries<IAggregate>()
				.Where(a => a.Entity.DomainEvents.Any())
				.Select(a => a.Entity);
			//retrieve the DomainEvents from the aggregate
			var domainEvents = aggregates.SelectMany(a => a.DomainEvents).ToList();
			//iterate all aggregates and clear the DomainEvents in each aggregate, to avoid duplicate dispatching
			aggregates.ToList().ForEach(a => a.ClearDomainEvents());
			//dispatch the DomainEvents
			foreach (var domainEvent in domainEvents)
			{
				await mediator.Publish(domainEvent);
			}
		}
	}
}
