using AutoRest.Api.ModelsRequest.Employee;
using AutoRest.Repositories.Contracts;
using FluentValidation;

namespace AutoRest.Api.Validators.Employee
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeRequestValidator : AbstractValidator<EmployeeRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public EmployeeRequestValidator(IPersonReadRepository personReadRepository)
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.EmployeeType)
                .NotNull()
                .WithMessage("Должность не должна быть null");

            RuleFor(x => x.PersonId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Личность не должна быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var person = await personReadRepository.GetByIdAsync(id, CancellationToken);
                    return person != null;
                })
                .WithMessage("Такой личности не существует!");
        }
    }
}
