﻿using FluentValidation;
using AutoRest.Api.ModelsRequest.Person;

namespace AutoRest.Api.Validators.Person
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonRequestValidator : AbstractValidator<PersonRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public PersonRequestValidator()
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Фамилия не должна быть пустой или null");

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым или null");
        }
    }
}
