using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Order.Application.Services;
using Order.Infrastructure;

namespace Order.API.Extensions;

public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddHealthChecks();
            
            services.AddDbContext<OrderDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("ConStr"));
            });

            services.AddServices();

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"Order API - {builder.Environment.EnvironmentName}",
                    Description = "An ASP.NET Core Web API for create order items",
                });
            });

        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
        }
    }