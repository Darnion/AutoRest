using AutoRest.Context.Contracts.Models;

namespace AutoRest.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="LoyaltyCard"/>
    /// </summary>
    public interface ILoyaltyCardReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="LoyaltyCard"/>
        /// </summary>
        Task<IReadOnlyCollection<LoyaltyCard>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="LoyaltyCard"/> по идентификатору
        /// </summary>
        Task<LoyaltyCard?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="LoyaltyCard"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, LoyaltyCard>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Узнать существует ли <see cref="LoyaltyCard"/> с таким номером
        /// </summary>
        Task<bool> AnyByNumberAsync(string number, CancellationToken cancellationToken);
    }
}
