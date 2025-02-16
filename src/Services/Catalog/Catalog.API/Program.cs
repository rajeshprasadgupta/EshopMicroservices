using Catalog.API.Data;


var builder = WebApplication.CreateBuilder(args);
//add services to the DI container
builder.Services.AddMediatR(config => {
	config.RegisterServicesFromAssembly(typeof(Program).Assembly);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddCarter();
var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddMarten(connectionString!).UseLightweightSessions();
if(builder.Environment.IsDevelopment())
{
	//Seed Initial Catalog (Product) Data
	builder.Services.InitializeMartenWith<CatalogInitialData>();
}
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();
//Configure the HTTP request pipeline
app.MapCarter();
app.UseExceptionHandler(configure =>{});
app.Run();
