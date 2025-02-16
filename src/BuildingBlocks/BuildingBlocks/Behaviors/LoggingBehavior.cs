using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
namespace BuildingBlocks.Behaviors
{
	public class LoggingBehavior<TRequest, TResponse>
		(ILogger<LoggingBehavior<TRequest,TResponse>> logger)
		: IPipelineBehavior<TRequest, TResponse>
		where TRequest : notnull, IRequest<TResponse>
		where TResponse : notnull
	{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			var requestName = typeof(TRequest).Name;
			var responseName = typeof(TResponse).Name;
			logger.LogInformation("Handling {Request} ({@Request})", requestName, request);
			var timer = new Stopwatch();
			timer.Start();
			var response = await next();
			timer.Stop();
			var timetaken = timer.ElapsedMilliseconds;
			logger.LogInformation("Handled {Request} with {Response} and took {timetaken} Milliseconds", requestName, responseName, timetaken);
			return response;
		}
	}
}
