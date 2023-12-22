using AutoRest.Common;
using AutoRest.Services.Automappers;
using AutoRest.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace AutoRest.Services
{
    public class ServiceModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IServiceAnchor>(ServiceLifetime.Scoped);
            service.RegisterAutoMapperProfile<ServiceProfile>();
        }
    }
}
