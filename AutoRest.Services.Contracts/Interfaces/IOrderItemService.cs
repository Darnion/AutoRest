using AutoRest.Services.Contracts.Models;
using AutoRest.Services.Contracts.ModelsRequest;

namespace AutoRest.Services.Contracts.Interfaces
{
    public interface IOrderItemService
    {
        /// <summary>
        /// Получить список всех <see cref="OrderItemModel"/>
        /// </summary>
        Task<IEnumerable<OrderItemModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="OrderItemModel"/> по идентификатору
        /// </summary>
        Task<OrderItemModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый заказ
        /// </summary>
        Task<OrderItemModel> AddAsync(OrderItemRequestModel request, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий заказ
        /// </summary>
        Task<OrderItemModel> EditAsync(OrderItemRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий заказ
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
