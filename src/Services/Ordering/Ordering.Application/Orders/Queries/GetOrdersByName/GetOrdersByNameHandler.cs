

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
			.Where(o=> o.OrderName.Value == query.Name)
			.OrderBy(o=> o.OrderName)
			.ToListAsync(cancellationToken);
		return new GetOrdersByNameResult(orders.ToOrderDtoList());
	}

}
