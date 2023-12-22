using FluentValidation;
using AutoRest.Api.ModelsRequest.MenuItem;

namespace AutoRest.Api.Validators.MenuItem
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateMenuItemRequestValidator : AbstractValidator<CreateMenuItemRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public CreateMenuItemRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Название не должно быть пустым или null");

            RuleFor(x => x.Cost)
                .NotNull()
                .WithMessage("Стоимость не должна быть null")
                .Must(x => x >= 0)
                .WithMessage("Стоимость не должна быть отрицательной");
        }
    }
}
