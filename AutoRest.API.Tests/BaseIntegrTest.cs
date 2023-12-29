using AutoMapper;
using AutoRest.Api.Tests.Infrastructures;
using AutoRest.Api.Infrastructures;
using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Context.Contracts;
using AutoRest.Services.Automappers;
using Xunit;

namespace AutoRest.API.Tests
{
    /// <summary>
    /// Базовый класс для тестов
    /// </summary>
    [Collection(nameof(AutoRestApiTestCollection))]
    public class BaseIntegrTest
    {
        protected readonly CustomWebApplicationFactory factory;
        protected readonly IAutoRestContext context;
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        public BaseIntegrTest(AutoRestApiFixture fixture)
        {
            factory = fixture.Factory;
            context = fixture.Context;
            unitOfWork = fixture.UnitOfWork;

            Profile[] profiles = { new ApiAutoMapperProfile(), new ServiceProfile() };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(profiles);
            });

            mapper = config.CreateMapper();
        }
    }
}