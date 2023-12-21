using Microsoft.OpenApi.Models;

namespace AutoRest.Api.Infrastructures
{
    static internal class DocumentExtensions
    {
        public static void GetSwaggerDocument(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Discipline", new OpenApiInfo { Title = "Сущность дисциплины", Version = "v1" });
                c.SwaggerDoc("Document", new OpenApiInfo { Title = "Сущность документы", Version = "v1" });
                c.SwaggerDoc("Employee", new OpenApiInfo { Title = "Сущность работники", Version = "v1" });
                c.SwaggerDoc("Group", new OpenApiInfo { Title = "Сущность группы", Version = "v1" });
                c.SwaggerDoc("Person", new OpenApiInfo { Title = "Сущность ученики", Version = "v1" });
                c.SwaggerDoc("TimeTableItem", new OpenApiInfo { Title = "Сущность элемент расписания", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "AutoRest.Api.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public static void GetSwaggerDocumentUI(this WebApplication app)
        {
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("Discipline/swagger.json", "Дисциплины");
                x.SwaggerEndpoint("Document/swagger.json", "Документы");
                x.SwaggerEndpoint("Employee/swagger.json", "Работники");
                x.SwaggerEndpoint("Group/swagger.json", "Группы");
                x.SwaggerEndpoint("Person/swagger.json", "Ученики");
                x.SwaggerEndpoint("TimeTableItem/swagger.json", "Элемент расписания");
            });
        }
    }
}
