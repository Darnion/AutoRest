using Microsoft.EntityFrameworkCore;
using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Common.Entity.Repositories;
using AutoRest.Context.Contracts.Models;
using AutoRest.Repositories.Contracts;

namespace AutoRest.Repositories.Implementations
{
    public class OrderItemReadRepository : IOrderItemReadRepository, IRepositoryAnchor
    {

        private readonly IDbRead reader;

        public OrderItemReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<OrderItem>> IOrderItemReadRepository.GetAllByDateAsync(DateTimeOffset startDate,
            DateTimeOffset endDate,
            CancellationToken cancellationToken)
            => reader.Read<OrderItem>()
                .NotDeletedAt()
                .Where(x => x.CreatedAt >= startDate &&
                            x.CreatedAt <= endDate)
                .OrderBy(x => x.CreatedAt)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<OrderItem?> IOrderItemReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<OrderItem>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
