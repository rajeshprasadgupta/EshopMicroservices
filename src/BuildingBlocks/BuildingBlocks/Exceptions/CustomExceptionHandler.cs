using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions
{
	public class CustomExceptionHandler
		(ILogger<CustomExceptionHandler> logger)
		: IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			logger.LogError(exception, exception.Message);
			(string Title, string Details, int StatusCode) = exception switch
			{
				InternalServerException e => (exception.GetType().Name, exception.Message, StatusCodes.Status500InternalServerError),
				BadRequestException e => (exception.GetType().Name, exception.Message, StatusCodes.Status400BadRequest),
				ValidationException e => (exception.GetType().Name, exception.Message, StatusCodes.Status400BadRequest),
				NotFoundException e => (exception.GetType().Name, exception.Message, StatusCodes.Status404NotFound),
				_ => (exception.GetType().Name, exception.Message, StatusCodes.Status500InternalServerError)
			};
			var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
			{
				Title = Title,
				Detail = Details,
				Status = StatusCode,
				Instance = httpContext.Request.Path
			};
			problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
			if (exception is ValidationException validationException)
			{
				problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
			}
			httpContext.Response.StatusCode = problemDetails.Status.Value;
			await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken:cancellationToken);
			return true;
		}
	}
}
