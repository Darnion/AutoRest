using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Common.Entity.Repositories;
using AutoRest.Context.Contracts.Models;
using AutoRest.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AutoRest.Repositories.Implementations
{
    public class LoyaltyCardReadRepository : ILoyaltyCardReadRepository, IRepositoryAnchor
    {

        private readonly IDbRead reader;

        public LoyaltyCardReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<LoyaltyCard>> ILoyaltyCardReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<LoyaltyCard>()
                .NotDeletedAt()
                .OrderBy(x => x.LoyaltyCardType)
                .ThenBy(x => x.Number)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<LoyaltyCard?> ILoyaltyCardReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<LoyaltyCard>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, LoyaltyCard>> ILoyaltyCardReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<LoyaltyCard>()
                .NotDeletedAt()
                .ByIds(ids)
                .ToDictionaryAsync(key => key.Id, cancellation);
    }
}
