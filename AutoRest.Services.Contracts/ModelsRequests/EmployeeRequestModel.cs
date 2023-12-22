using AutoRest.Context.Contracts.Enums;
using AutoRest.Services.Contracts.Models;

namespace AutoRest.Services.Contracts.ModelsRequest
{
    public class EmployeeRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <inheritdoc cref="EmployeeTypes"/>
        public EmployeeTypes EmployeeType { get; set; }

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public Guid PersonId { get; set; }

    }
}
