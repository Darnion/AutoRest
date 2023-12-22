using AutoRest.Services.Contracts.Models;
using AutoRest.Services.Contracts.ModelsRequest;

namespace AutoRest.Services.Contracts.Interfaces
{
    public interface ILoyaltyCardService
    {
        /// <summary>
        /// Получить список всех <see cref="LoyaltyCardModel"/>
        /// </summary>
        Task<IEnumerable<LoyaltyCardModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="LoyaltyCardModel"/> по идентификатору
        /// </summary>
        Task<LoyaltyCardModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую карту лояльности
        /// </summary>
        Task<LoyaltyCardModel> AddAsync(LoyaltyCardRequestModel request, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую карту лояльности
        /// </summary>
        Task<LoyaltyCardModel> EditAsync(LoyaltyCardRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую карту лояльности
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
