using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Context.Contracts.Models;
using AutoRest.Context.Contracts;
using Microsoft.EntityFrameworkCore;
using AutoRest.Context.Configuration;

namespace AutoRest.Context
{
    /// <summary>
    /// Контекст работы с БД
    /// </summary>
    /// <remarks>
    /// 1) dotnet tool install --global dotnet-ef
    /// 2) dotnet tool update --global dotnet-ef
    /// 3) dotnet ef migrations add [name] --project AutoRest.Context\AutoRest.Context.csproj
    /// 4) dotnet ef database update --project AutoRest.Context\AutoRest.Context.csproj
    /// 5) dotnet ef database update [targetMigrationName] --AutoRest.Context\AutoRest.Context.csproj
    /// </remarks>
    public class AutoRestContext : DbContext,
        IAutoRestContext,
        IDbRead,
        IDbWriter,
        IUnitOfWork
    {

        public DbSet<LoyaltyCard> LoyaltyCards { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Table> Tables { get; set; }

        /// <summary>
        /// Инициализация контекста
        /// </summary>
        public AutoRestContext(DbContextOptions<AutoRestContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IContextConfigurationAnchor).Assembly);
        }

        IQueryable<TEntity> IDbRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        void IDbWriter.Add<TEntities>(TEntities entity)
            => base.Entry(entity).State = EntityState.Added;

        void IDbWriter.Update<TEntities>(TEntities entity)
              => base.Entry(entity).State = EntityState.Modified;

        void IDbWriter.Delete<TEntities>(TEntities entity)
              => base.Entry(entity).State = EntityState.Deleted;


        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            SkipTracker();
            return count;
        }

        public void SkipTracker()
        {
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
