using MediatR;
namespace BuildingBlocks.CQRS
{
	//Interfaces for Query Handler., inherits IRequestHandler from MediatR, for handling query operations
	public interface IQueryHandler<in TQuery, TResponse> 
		: IRequestHandler<TQuery, TResponse>
		where TQuery : IQuery<TResponse>
		where TResponse : notnull
	{
	}
}
