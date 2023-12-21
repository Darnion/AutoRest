using Microsoft.Extensions.DependencyInjection;
using AutoRest.Common;
using AutoRest.Shared;

namespace AutoRest.Repositories
{
    public class RepositoryModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}
