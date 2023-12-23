namespace AutoRest.Api.Tests.Infrastructures
{
    static internal class WebHostBuilderHelper
    {
        /// <summary>
        /// Конфигурирование IWebHostBuilder
        /// </summary>
        public static void ConfigureTestAppConfiguration(this IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((_, config) =>
            {
                var projectDir = Directory.GetCurrentDirectory();
                var configPath = Path.Combine(projectDir,
                    $"appsettings.{CustomWebApplicationFactory.EnvironmentName}.json");
                config.AddJsonFile(configPath).AddEnvironmentVariables();
            });
        }
    }
}
