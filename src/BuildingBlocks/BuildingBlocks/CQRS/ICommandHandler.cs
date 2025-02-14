using MediatR;

namespace BuildingBlocks.CQRS
{
	//Interfaces for Command Handler., inherits IRequestHandler from MediatR, for handling command operations
	//This handler is used for commands that do not return a response, so the response type is Unit(void) type
	public interface ICommandHandler<in TCommand>
		: IRequestHandler<TCommand, Unit>
		where TCommand : ICommand<Unit>
	{ 
	}

	//This handler is used for commands that return a response
	public interface ICommandHandler<in TCommand, TResponse>
		: IRequestHandler<TCommand, TResponse>
		where TCommand : ICommand<TResponse>
		where TResponse : notnull
	{
	}
}
