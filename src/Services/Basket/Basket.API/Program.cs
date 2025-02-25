using Basket.API.Data;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Messaging.MassTransit;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
//Add Services to the container
//Application Services
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(typeof(Program).Assembly);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
var connectionString = builder.Configuration.GetConnectionString("Database");
//Data Services
builder.Services.AddMarten(connectionString!).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = redisConnectionString;
});
//decorate the repository with the cached repository using Scrutor DI package
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
//Add Grpc service
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
	options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
}).ConfigurePrimaryHttpMessageHandler(options => 
{
	var handler = new HttpClientHandler()
	{
		ServerCertificateCustomValidationCallback =
		HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
	};
	return handler;
});
//Async Communication services
builder.Services.AddMessageBroker(builder.Configuration);
//Cross-Cussting Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
//add health check for postgres
builder.Services.AddHealthChecks().AddNpgSql(connectionString!);
//add health check for redis
builder.Services.AddHealthChecks().AddRedis(redisConnectionString!);
var app = builder.Build();
// Configure the HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(configure => { });
app.UseHealthChecks("/health", new HealthCheckOptions
{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();
