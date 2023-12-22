using AutoRest.Services.Contracts.Models;
using AutoRest.Services.Contracts.ModelsRequest;

namespace AutoRest.Services.Contracts.Interfaces
{
    public interface IMenuItemService
    {
        /// <summary>
        /// Получить список всех <see cref="MenuItemModel"/>
        /// </summary>
        Task<IEnumerable<MenuItemModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="MenuItemModel"/> по идентификатору
        /// </summary>
        Task<MenuItemModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую позицию меню
        /// </summary>
        Task<MenuItemModel> AddAsync(MenuItemRequestModel request, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую позицию меню
        /// </summary>
        Task<MenuItemModel> EditAsync(MenuItemRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую позицию меню
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
