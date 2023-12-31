﻿using AutoRest.Api.ModelsRequest.LoyaltyCard;
using FluentValidation;

namespace AutoRest.Api.Validators.LoyaltyCard
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateLoyaltyCardRequestValidator : AbstractValidator<CreateLoyaltyCardRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public CreateLoyaltyCardRequestValidator()
        {
            RuleFor(x => x.LoyaltyCardType)
                .NotNull()
                .WithMessage("Тип карты не должен быть пустым или null");

            RuleFor(x => x.Number)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер карты не должен быть пустым или null")
                .MaximumLength(40)
                .WithMessage("Слишком много символов. Должно быть не более 40."); ;
        }
    }
}
