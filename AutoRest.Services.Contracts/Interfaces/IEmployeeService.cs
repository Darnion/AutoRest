using AutoRest.Services.Contracts.Models;
using AutoRest.Services.Contracts.ModelsRequest;

namespace AutoRest.Services.Contracts.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Получить список всех <see cref="EmployeeModel"/>
        /// </summary>
        Task<IEnumerable<EmployeeModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="EmployeeModel"/> по идентификатору
        /// </summary>
        Task<EmployeeModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет нового работника
        /// </summary>
        Task<EmployeeModel> AddAsync(EmployeeRequestModel request, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующего работника
        /// </summary>
        Task<EmployeeModel> EditAsync(EmployeeRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующего работника
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
