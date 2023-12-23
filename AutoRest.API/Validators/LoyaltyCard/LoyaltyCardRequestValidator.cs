using AutoRest.Api.ModelsRequest.LoyaltyCard;
using FluentValidation;

namespace AutoRest.Api.Validators.LoyaltyCard
{
    /// <summary>
    /// 
    /// </summary>
    public class LoyaltyCardRequestValidator : AbstractValidator<LoyaltyCardRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public LoyaltyCardRequestValidator()
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.LoyaltyCardType)
                .NotNull()
                .WithMessage("Тип карты не должен быть пустым или null");

            RuleFor(x => x.Number)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер карты не должен быть пустым или null")
                .MaximumLength(40)
                .WithMessage("Слишком много символов. Должно быть не более 40.");
        }
    }
}