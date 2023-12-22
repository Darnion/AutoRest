using AutoRest.Common;
using AutoRest.Shared;
using Microsoft.Extensions.DependencyInjection;

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
