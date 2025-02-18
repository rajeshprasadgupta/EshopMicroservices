namespace Catalog.API.Products.GetProductByCategory
{
	public record GetProductByCategoryResonse(IEnumerable<Product> Products);

	public class GetProductByCatagoryEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/products/category/{category}", async (ISender sender, string category) =>
			{
				var result = await sender.Send(new GetProductByCategoryQuery(category));
				var response = result.Adapt<GetProductByCategoryResonse>();
				return Results.Ok(response);
			})
			.WithName("GetProductByCategory")
			.Produces<GetProductByCategoryResonse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithDescription("Get Products by Category")
			.WithSummary("Get Products by Category");
		}
	}
	
}
