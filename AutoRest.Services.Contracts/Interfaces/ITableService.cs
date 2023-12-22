using AutoRest.Services.Contracts.Models;
using AutoRest.Services.Contracts.ModelsRequest;

namespace AutoRest.Services.Contracts.Interfaces
{
    public interface ITableService
    {
        /// <summary>
        /// Получить список всех <see cref="TableModel"/>
        /// </summary>
        Task<IEnumerable<TableModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="TableModel"/> по идентификатору
        /// </summary>
        Task<TableModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый столик
        /// </summary>
        Task<TableModel> AddAsync(TableRequestModel request, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий столик
        /// </summary>
        Task<TableModel> EditAsync(TableRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий столик
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
