namespace Ordering.Infrastructure.Data
{
	//Add Test data to the database tables
	internal static class InitialSeedData
	{

		internal static Customer[] GetCustomers()
		{
			var customers = new List<Customer>
			{
				Customer.Create(CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")), "rajesh", "rajesh@gmail.com"),
				Customer.Create(CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")), "raju", "raju@gmail.com")
			};
			return customers.ToArray();
		}

		internal static Order[] GetOrders()
		{
			var address1 = Address.Of("rajesh", "gupta", "rajesh@gmail.com", "Somewhere", "Country 1", "City 1", "100001");
			var address2 = Address.Of("raju", "ranjan", "raju@gmail.com", "Nowhere", "Country 2", "City 2", "100002");

			var payment1 = Payment.Of("rajesh", "5555555555554444", "12/28", "355", 1);
			var payment2 = Payment.Of("raju", "8885555555554444", "06/30", "222", 2);

			var order1 = Order.Create(
							OrderId.Of(Guid.NewGuid()),
							CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
							OrderName.Of("ORD_1"),
							address1,
							address1,
							payment1);
			order1.AddOrderItem(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), 2, 100);
			order1.AddOrderItem(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), 1, 200);

			var order2 = Order.Create(
							OrderId.Of(Guid.NewGuid()),
							CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")),
							OrderName.Of("ORD_2"),
							address2,
							address2,
							payment2);
			order2.AddOrderItem(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), 1, 300);
			order2.AddOrderItem(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), 2, 400);
			return new Order[] { order1, order2 };	
		}

		internal static Product[] GetProducts()
		{
			var products = new List<Product>
			{
				Product.Create(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), "Product 1", 100),
				Product.Create(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), "Product 2", 200),
				Product.Create(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), "Product 3", 300),
				Product.Create(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), "Product 4", 400)
			};
			return products.ToArray();
		}
	}
}
