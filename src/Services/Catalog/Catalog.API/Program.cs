using Catalog.API.Products.CreateProduct;
using Weasel.Core;


var builder = WebApplication.CreateBuilder(args);
//add services to the DI container
builder.Services.AddCarter();
builder.Services.AddMediatR(config => {
	config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddMarten(connectionString!).UseLightweightSessions();
var app = builder.Build();
//Configure the HTTP request pipeline
app.MapCarter();
app.Run();
