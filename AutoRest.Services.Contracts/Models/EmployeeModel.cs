using AutoRest.Context.Contracts.Enums;

namespace AutoRest.Context.Contracts.Models
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
        public PersonModel? Person { get; set; }
    }
}