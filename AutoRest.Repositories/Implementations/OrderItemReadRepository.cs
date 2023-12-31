﻿using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Common.Entity.Repositories;
using AutoRest.Context.Contracts.Models;
using AutoRest.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AutoRest.Repositories.Implementations
{
    public class OrderItemReadRepository : IOrderItemReadRepository, IRepositoryAnchor
    {

        private readonly IDbRead reader;

        public OrderItemReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<OrderItem>> IOrderItemReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<OrderItem>()
                .NotDeletedAt()
                .OrderBy(x => x.CreatedAt)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<OrderItem?> IOrderItemReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<OrderItem>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
