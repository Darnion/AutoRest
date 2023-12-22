using AutoRest.Context.Contracts.Models;

namespace AutoRest.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="MenuItem"/>
    /// </summary>
    public interface IMenuItemReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="MenuItem"/>
        /// </summary>
        Task<IReadOnlyCollection<MenuItem>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="MenuItem"/> по идентификатору
        /// </summary>
        Task<MenuItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="MenuItem"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, MenuItem>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Узнать существует ли <see cref="MenuItem"/> с таким ид
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
