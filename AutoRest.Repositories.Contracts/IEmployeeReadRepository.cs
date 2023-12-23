using AutoRest.Context.Contracts.Models;

namespace AutoRest.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Employee"/>
    /// </summary>
    public interface IEmployeeReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Employee"/>
        /// </summary>
        Task<IReadOnlyCollection<Employee>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Employee"/> по идентификатору
        /// </summary>
        Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Employee"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Employee>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Получить список <see cref="Person"/> по идентификаторам сотрудников
        /// </summary>
        Task<Dictionary<Guid, Person>> GetPersonByEmployeeIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Узнать существует ли <see cref="Employee"/> с таким ид
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Узнать существует ли <see cref="Employee"/> с таким ид
        /// </summary>
        Task<bool> IsTypeAllowedAsync(Guid id, CancellationToken cancellationToken);
    }
}
