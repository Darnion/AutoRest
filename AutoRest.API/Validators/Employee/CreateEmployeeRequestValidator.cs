using FluentValidation;
using TimeTable203.Api.ModelsRequest.Employee;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Api.Validators.Employee
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public CreateEmployeeRequestValidator(IPersonReadRepository personReadRepository)
        {

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
