using Microsoft.EntityFrameworkCore;
using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Common.Entity.Repositories;
using AutoRest.Context.Contracts.Models;
using AutoRest.Repositories.Contracts;

namespace AutoRest.Repositories.Implementations
{
    public class MenuItemReadRepository : IMenuItemReadRepository, IRepositoryAnchor
    {

        private readonly IDbRead reader;

        public MenuItemReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<MenuItem>> IMenuItemReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<MenuItem>()
                .NotDeletedAt()
                .OrderBy(x => x.Cost)
                .ThenBy(x => x.Title)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<MenuItem?> IMenuItemReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<MenuItem>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, MenuItem>> IMenuItemReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<MenuItem>()
                .NotDeletedAt()
                .ByIds(ids)
                .ToDictionaryAsync(key => key.Id, cancellation);
    }
}
