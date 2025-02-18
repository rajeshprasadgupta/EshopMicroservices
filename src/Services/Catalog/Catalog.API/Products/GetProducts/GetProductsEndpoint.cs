namespace Catalog.API.Products.GetProducts
{
	public record GetProductsResponse(IEnumerable<Product> Products);

	public record GetProductRequest(int? PageNumber = 1, int? PageSize =5);
	public class GetProductsEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/products", async ([AsParameters] GetProductRequest request, ISender sender) =>
			{
				var query = request.Adapt<GetProductsQuery>();
				var result = await sender.Send(query);
				var response = result.Adapt<GetProductsResponse>();
				return Results.Ok(response);
			})
			.WithName("GetProducts")
			.Produces<GetProductsResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithDescription("Get Products")
			.WithSummary("Get Products");
		}
	}
	
}
