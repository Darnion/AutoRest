using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Common.Entity.Repositories;
using AutoRest.Context.Contracts.Models;
using AutoRest.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AutoRest.Repositories.Implementations
{
    public class TableReadRepository : ITableReadRepository, IRepositoryAnchor
    {

        private readonly IDbRead reader;

        public TableReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Table>> ITableReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Table>()
                .NotDeletedAt()
                .OrderBy(x => x.Number)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Table?> ITableReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Table>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Table>> ITableReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Table>()
                .NotDeletedAt()
                .ByIds(ids)
                .ToDictionaryAsync(key => key.Id, cancellation);

        Task<bool> ITableReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Table>()
                .NotDeletedAt()
                .ById(id)
                .AnyAsync(cancellationToken);
    }
}
