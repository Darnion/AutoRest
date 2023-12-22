using AutoRest.Context.Contracts.Enums;
using AutoRest.Context.Contracts.Models;

namespace AutoRest.Repositories.Contracts
{

    /// <summary>
    /// Репозиторий чтения <see cref="Person"/>
    /// </summary>
    public interface IPersonReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Person"/>
        /// </summary>
        Task<IReadOnlyCollection<Person>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Person"/> по идентификатору
        /// </summary>
        Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Узнать существует ли <see cref="Person"/> с таким ид
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Person"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Person>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Получить список всех <see cref="Person"/> по должности
        /// </summary>
        //Task<IReadOnlyCollection<Person>> GetAllByEmployeeTypeAsync(EmployeeTypes EmployeeType, CancellationToken cancellationToken);
    }

}
