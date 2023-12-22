using Microsoft.OpenApi.Models;

namespace AutoRest.Api.Infrastructures
{
    static internal class DocumentExtensions
    {
        public static void GetSwaggerDocument(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("LoyaltyCard", new OpenApiInfo { Title = "Сущность карты лояльности", Version = "v1" });
                c.SwaggerDoc("MenuItem", new OpenApiInfo { Title = "Сущность позиции меню", Version = "v1" });
                c.SwaggerDoc("Employee", new OpenApiInfo { Title = "Сущность работники", Version = "v1" });
                c.SwaggerDoc("Table", new OpenApiInfo { Title = "Сущность столики", Version = "v1" });
                c.SwaggerDoc("Person", new OpenApiInfo { Title = "Сущность личности", Version = "v1" });
                c.SwaggerDoc("OrderItem", new OpenApiInfo { Title = "Сущность заказы", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "AutoRest.Api.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public static void GetSwaggerDocumentUI(this WebApplication app)
        {
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("LoyaltyCard/swagger.json", "Карты лояльности");
                x.SwaggerEndpoint("MenuItem/swagger.json", "Позиции меню");
                x.SwaggerEndpoint("Employee/swagger.json", "Работники");
                x.SwaggerEndpoint("Table/swagger.json", "Столики");
                x.SwaggerEndpoint("Person/swagger.json", "Личности");
                x.SwaggerEndpoint("OrderItem/swagger.json", "Заказы");
            });
        }
    }
}
