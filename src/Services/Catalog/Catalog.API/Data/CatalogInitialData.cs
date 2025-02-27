namespace Catalog.API.Data
{
	using Marten.Schema;
	public class CatalogInitialData
		(ILogger<CatalogInitialData> logger)
		: IInitialData
	{
		public async Task Populate(IDocumentStore store, CancellationToken cancellation)
		{
			using (var session = store.LightweightSession())
			{
				if (await session.Query<Product>().AnyAsync())
				{
					logger.LogInformation("Products are already populated, so skipping inserting db with Initial products");
					return;
				}
				session.Store<Product>(GetProducts());
				await session.SaveChangesAsync(cancellation);
			}
		}

		private IEnumerable<Product> GetProducts()
		{
			var list = new List<Product>
			{
				new Product
				{
					Name = "Product 1",
					Description = "Description 1",
					Price = 100,
					ImageFile = "ImageFile 1",
					Category = new List<string> { "Category 1", "Category 2" }
				},
				new Product
				{
					Name = "Product 2",
					Description = "Description 2",
					Price = 200,
					ImageFile = "ImageFile 2",
					Category = new List<string> { "Category 2", "Category 3" }
				},
				new Product
				{
					Name = "Product 3",
					Description = "Description 3",
					Price = 300,
					ImageFile = "ImageFile 3",
					Category = new List<string> { "Category 3", "Category 4" }
				},
				new Product
				{
					Name = "Product 4",
					Description = "Description 4",
					Price = 400,
					ImageFile = "ImageFile 4",
					Category = new List<string> { "Category 4", "Category 5" }
				},
				new Product
				{
					Name = "Product 5",
					Description = "Description 5",
					Price = 500,
					ImageFile = "ImageFile 5",
					Category = new List<string> { "Category 4", "Category 5" }
				},
				new Product
				{
					Name = "Product 6",
					Description = "Description 6",
					Price = 500,
					ImageFile = "ImageFile 6",
					Category = new List<string> { "Category 1", "Category 5" }
				},
				new Product
				{
					Name = "Product 7",
					Description = "Description 7",
					Price = 500,
					ImageFile = "ImageFile 7",
					Category = new List<string> { "Category 4", "Category 1" }
				}
			};
			return list;
		}
	}
	
}
