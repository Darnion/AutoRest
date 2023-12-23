using Microsoft.EntityFrameworkCore;
using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Context;
using AutoRest.Context.Contracts;
using Xunit;

namespace AutoRest.Api.Tests.Infrastructures
{
    public class AutoRestApiFixture : IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory factory;
        private AutoRestContext? autoRestContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AutoRestApiFixture"/>
        /// </summary>
        public AutoRestApiFixture()
        {
            factory = new CustomWebApplicationFactory();
        }

        Task IAsyncLifetime.InitializeAsync() => AutoRestContext.Database.MigrateAsync();

        async Task IAsyncLifetime.DisposeAsync()
        {
            await AutoRestContext.Database.EnsureDeletedAsync();
            await AutoRestContext.Database.CloseConnectionAsync();
            await AutoRestContext.DisposeAsync();
            await factory.DisposeAsync();
        }

        public CustomWebApplicationFactory Factory => factory;

        public IAutoRestContext Context => AutoRestContext;

        public IUnitOfWork UnitOfWork => AutoRestContext;

        internal AutoRestContext AutoRestContext
        {
            get
            {
                if (autoRestContext != null)
                {
                    return autoRestContext;
                }

                var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                autoRestContext = scope.ServiceProvider.GetRequiredService<AutoRestContext>();
                return autoRestContext;
            }
        }
    }
}
