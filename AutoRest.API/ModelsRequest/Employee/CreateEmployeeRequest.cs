using AutoRest.Api.Models.Enums;
using AutoRest.Services.Contracts.Models;

namespace AutoRest.Api.ModelsRequest.Employee
{
    public class CreateEmployeeRequest
    {
        /// <inheritdoc cref="EmployeeTypesResponse"/>
        public EmployeeTypesResponse EmployeeType { get; set; }

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public Guid PersonId { get; set; }
    }
}
