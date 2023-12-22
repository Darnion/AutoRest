using AutoRest.Services.Contracts.Models.Enums;

namespace AutoRest.Services.Contracts.Models
{
    /// <summary>
    /// Модель "Сотрудник"
    /// </summary>
    public class EmployeeModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <inheritdoc cref="EmployeeTypesModel"/>
        public EmployeeTypesModel EmployeeType { get; set; }

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public PersonModel Person { get; set; }
    }
}