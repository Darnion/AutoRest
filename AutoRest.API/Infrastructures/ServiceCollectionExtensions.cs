using AutoRest.Api.Infrastructures.Validator;
using AutoRest.Common;
using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Context;
using AutoRest.Repositories;
using AutoRest.Services;
using AutoRest.Shared;

namespace AutoRest.Api.Infrastructures
{
    static internal class ServiceCollectionExtensions
    {
        public static void AddDependencies(this IServiceCollection service)
        {
            service.AddTransient<IDateTimeProvider, DateTimeProvider>();
            service.AddTransient<IDbWriterContext, DbWriterContext>();
            service.AddTransient<IApiValidatorService, ApiValidatorService>();
            service.RegisterAutoMapperProfile<ApiAutoMapperProfile>();

            service.RegisterModule<ServiceModule>();
            service.RegisterModule<RepositoryModule>();
            service.RegisterModule<ContextModule>();

            service.RegisterAutoMapper();
        }
    }
}
