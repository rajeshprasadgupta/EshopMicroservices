using Catalog.API.Products.CreateProduct;

var builder = WebApplication.CreateBuilder(args);
//add sercices to the DI container
builder.Services.AddCarter(configurator: c => 
{
	c.WithModule<CreateProductEndpoint>();
});
builder.Services.AddMediatR(config => {
	config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
var app = builder.Build();
//Configure the HTTP request pipeline
app.MapCarter();
app.Run();
