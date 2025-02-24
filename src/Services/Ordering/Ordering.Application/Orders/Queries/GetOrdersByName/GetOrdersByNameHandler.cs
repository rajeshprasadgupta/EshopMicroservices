

namespace Ordering.Application.Orders.Queries.GetOrdersByName;
public class GetOrdersByNameHandler
	(IApplicationDbContext dbcontext)
	: IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
	public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
	{
		var orders = await dbcontext.Orders
			.Include(o => o.OrderItems)
			.AsNoTracking()
			.Where(o=> o.OrderName.Value.Contains(query.Name))
			.OrderBy(o=> o.OrderName.Value)
			.ToListAsync(cancellationToken);
		return new GetOrdersByNameResult(orders.ToOrderDtoList());
	}

}
