using AutoMapper;
using AutoRest.Services.Automappers;
using Xunit;

namespace AutoRest.Services.Tests
{
    public class MapperTest
    {
        /// <summary>
        /// Тесты на маппер
        /// </summary>
        [Fact]
        public void TestMap()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });

            config.AssertConfigurationIsValid();
        }
    }
}
