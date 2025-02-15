﻿namespace Catalog.API.Products.GetProductById
{
	
	public record GetProductByIdResponse(Product Product);
	public class GetProductByIdEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/products/{id}", async (ISender sender, Guid id) =>
			{
				var result = await sender.Send(new GetProductByIdQuery(id));
				var response = result.Adapt<GetProductByIdResponse>();
				return Results.Ok(response);
			})
			.WithName("GetProductById")
			.Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithDescription("Get Product by Id")
			.WithSummary("Get Product by Id");
		}
	}
	
}
