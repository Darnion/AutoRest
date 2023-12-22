using Microsoft.EntityFrameworkCore;
using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Common.Entity.Repositories;
using AutoRest.Context.Contracts.Models;
using AutoRest.Repositories.Contracts;
using AutoRest.Context.Contracts.Enums;

namespace AutoRest.Repositories.Implementations
{
    public class PersonReadRepository : IPersonReadRepository, IRepositoryAnchor
    {

        private readonly IDbRead reader;

        public PersonReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Person>> IPersonReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Person>()
                .NotDeletedAt()
                .OrderBy(x => x.LastName)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Person?> IPersonReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Person>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Person>> IPersonReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Person>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ThenBy(x => x.Patronymic)
                .ToDictionaryAsync(key => key.Id, cancellation);

        Task<bool> IPersonReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Person>()
                .NotDeletedAt()
                .ById(id)
                .AnyAsync(cancellationToken);

        //Task<IReadOnlyCollection<Person>> IPersonReadRepository.GetAllByEmployeeTypeAsync(EmployeeTypes EmployeeType, CancellationToken cancellationToken)
        //    => reader.Read<Person>()
        //        .NotDeletedAt()
        //        .Where(x => x.)
        //        .AnyAsync(cancellationToken);
    }
}
