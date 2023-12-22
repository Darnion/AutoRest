using AutoRest.Services.Contracts.Models;
using AutoRest.Services.Contracts.ModelsRequest;

namespace AutoRest.Services.Contracts.Interface
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
        /// Добавляет нового учителя
        /// </summary>
        Task<EmployeeModel> AddAsync(EmployeeRequestModel request, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующего учителя
        /// </summary>
        Task<EmployeeModel> EditAsync(EmployeeRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующего учителя
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
