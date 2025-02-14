namespace Catalog.API.Models
{
	//Product model is the Document model for Document Database
	// Document Database used here is Postgres with Marten library
	public class Product
	{
		public Guid Id { get; set; }

		public string Name { get; set; } = default!;

		//Product has 1:N relation with Category
		public List<string> Category { get; set; } = new();

		public string Description { get; set; } = default!;

		public string ImageFile { get; set; } = default!;

		public decimal Price { get; set; }
	}
}
