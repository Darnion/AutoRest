using AutoRest.Context.Contracts.Models;

namespace AutoRest.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Table"/>
    /// </summary>
    public interface ITableReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Table"/>
        /// </summary>
        Task<IReadOnlyCollection<Table>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Table"/> по идентификатору
        /// </summary>
        Task<Table?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Table"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Table>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Узнать существует ли <see cref="Table"/> с таким ид
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
