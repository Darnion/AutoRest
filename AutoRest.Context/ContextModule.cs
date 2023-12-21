using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AutoRest.Common;
using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Context.Contracts;

namespace AutoRest.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.TryAddScoped<IAutoRestContext>(provider => provider.GetRequiredService<AutoRestContext>());
            service.TryAddScoped<IDbRead>(provider => provider.GetRequiredService<AutoRestContext>());
            service.TryAddScoped<IDbWriter>(provider => provider.GetRequiredService<AutoRestContext>());
            service.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AutoRestContext>());
        }
    }
}
