using Order.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureApplicationServices(builder);

var app = builder.Build();

app.ConfigureRequestPipeline();

app.Run();