using AutoRest.Api.ModelsRequest.MenuItem;
using FluentValidation;

namespace AutoRest.Api.Validators.MenuItem
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuItemRequestValidator : AbstractValidator<MenuItemRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public MenuItemRequestValidator()
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

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