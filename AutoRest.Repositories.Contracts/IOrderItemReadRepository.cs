using AutoRest.Context.Contracts.Models;

namespace AutoRest.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="OrderItem"/>
    /// </summary>
    public interface IOrderItemReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="OrderItem"/>
        /// </summary>
        Task<IReadOnlyCollection<OrderItem>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="OrderItem"/> по идентификатору
        /// </summary>
        Task<OrderItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
