using Microsoft.AspNetCore.RateLimiting;
var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
// Add rate limit of maximum 5 requests per 10 seconds
builder.Services.AddRateLimiter(rlOptions => {
	rlOptions.AddFixedWindowLimiter("fixed", options => {
		options.PermitLimit = 5;
		options.Window = TimeSpan.FromSeconds(10);
	});
});
var app = builder.Build();
//Confifure the HTTP request pipeline
app.UseRateLimiter();
app.MapReverseProxy();
app.Run();
