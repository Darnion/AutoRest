using AutoRest.Api.Models.Enums;

namespace AutoRest.Api.Models
{
    /// <summary>
    /// Модель ответа сущности работников
    /// </summary>
    public class EmployeeResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <inheritdoc cref="EmployeeTypesResponse"/>
        public string EmployeeType { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор работника
        /// </summary>
        public Guid PersonId { get; set; }
    }
}
