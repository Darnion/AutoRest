using AutoRest.Api.ModelsRequest.Person;
using FluentValidation;

namespace AutoRest.Api.Validators.Person
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
                .WithMessage("Слишком много символов");

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым или null")
                .MaximumLength(80)
                .WithMessage("Слишком много символов");

            RuleFor(x => x.Patronymic)
                .MaximumLength(80);
        }
    }
}
