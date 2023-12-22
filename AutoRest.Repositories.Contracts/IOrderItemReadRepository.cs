using AutoRest.Context.Contracts.Models;

namespace AutoRest.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="OrderItem"/>
    /// </summary>
    public interface IOrderItemReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="OrderItem"/> входящих в период между <see langword="startDate"/> и <see langword="endDate"/> включительно
        /// </summary>
        Task<IReadOnlyCollection<OrderItem>> GetAllByDateAsync(DateTimeOffset startDate, DateTimeOffset endDate, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="OrderItem"/> по идентификатору
        /// </summary>
        Task<OrderItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
