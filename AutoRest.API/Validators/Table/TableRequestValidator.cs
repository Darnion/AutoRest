﻿using AutoRest.Api.ModelsRequest.Table;
using FluentValidation;

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
                .WithMessage("Номер столика не должен быть пустым или null")
                .MaximumLength(20)
                .WithMessage("Слишком много символов. Должно быть не более 20.");
        }
    }
}