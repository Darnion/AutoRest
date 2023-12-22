using FluentValidation;
using TimeTable203.Api.ModelsRequest.Person;

namespace TimeTable203.Api.Validators.Person
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePersonRequestValidator : AbstractValidator<CreatePersonRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public CreatePersonRequestValidator()
        {

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Фамилия не должна быть пустой или null")
                .MaximumLength(80)
                .WithMessage("Фамилия больше 80 символов");

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым или null")
                .MaximumLength(80)
                .WithMessage("Имя больше 80 символов");

            RuleFor(x => x.Email)
               .NotNull()
               .NotEmpty()
               .WithMessage("Почта не должна быть пустой или null")
               .EmailAddress()
               .WithMessage("Требуется действительная почта!");

            RuleFor(x => x.Phone)
             .NotNull()
             .NotEmpty()
             .WithMessage("Телефон не должна быть пустой или null");
        }
    }
}
