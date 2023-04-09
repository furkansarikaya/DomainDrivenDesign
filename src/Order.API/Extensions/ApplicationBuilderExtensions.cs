using Microsoft.EntityFrameworkCore;
using Order.API.Middlewares;
using Order.Infrastructure;

namespace Order.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ConfigureRequestPipeline(this WebApplication app)
    {
        app.MapHealthChecks("/healthz");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // migrate any database changes on startup (includes initial db creation)
        using (var scope = app.Services.CreateScope())
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
            dataContext.Database.Migrate();
        }

        app.UseSwagger(options =>
        {
            options.SerializeAsV2 = true;
            options.RouteTemplate = "swagger/{documentName}/swagger.json";
        });

        app.UseSwaggerUI(so =>
        {
            so.DocumentTitle = $"Order API - {app.Environment.EnvironmentName}";
            so.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            so.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API");
        });

        app.UseHttpsRedirection();

        app.UseMiddleware<LoggingMiddleware>();

        app.UseAuthorization();

        app.MapControllers();
    }
}