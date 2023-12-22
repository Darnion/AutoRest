using FluentValidation;
using AutoRest.Api.ModelsRequest.Employee;
using AutoRest.Repositories.Contracts;

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
                .WithMessage("Тип документа не должен быть null");

            RuleFor(x => x.PersonId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Персона не должна быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var person = await personReadRepository.GetByIdAsync(id, CancellationToken);
                    return person != null;
                })
                .WithMessage("Такой персоны не существует!");
        }
    }
}
