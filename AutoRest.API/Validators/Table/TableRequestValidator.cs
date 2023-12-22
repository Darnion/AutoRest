using FluentValidation;
using AutoRest.Api.ModelsRequest.Table;

namespace AutoRest.Api.Validators.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class TableRequestValidator : AbstractValidator<TableRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public TableRequestValidator()
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.Number)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер столика не должен быть пустым или null");
        }
    }
}