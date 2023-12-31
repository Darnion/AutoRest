﻿using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Common.Entity.Repositories;
using AutoRest.Context.Contracts.Enums;
using AutoRest.Context.Contracts.Models;
using AutoRest.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AutoRest.Repositories.Implementations
{
    public class EmployeeReadRepository : IEmployeeReadRepository, IRepositoryAnchor
    {

        private readonly IDbRead reader;

        public EmployeeReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Employee>> IEmployeeReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .OrderBy(x => x.EmployeeType)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Employee?> IEmployeeReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Employee>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Employee>> IEmployeeReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .ByIds(ids)
                .ToDictionaryAsync(key => key.Id, cancellation);

        public Task<Dictionary<Guid, Person>> GetPersonByEmployeeIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .ByIds(ids)
                .Select(x => new
                {
                    x.Id,
                    x.Person,
                })
                .ToDictionaryAsync(key => key.Id, val => val.Person, cancellation);

        Task<bool> IEmployeeReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .ById(id)
                .AnyAsync(cancellationToken);

        Task<bool> IEmployeeReadRepository.IsTypeNotAllowedAsync(Guid id, CancellationToken cancellationToken)
           => reader.Read<Employee>()
               .NotDeletedAt()
               .ById(id)
               .AnyAsync(x => x.EmployeeType < EmployeeTypes.Сashier, cancellationToken);
    }
}
