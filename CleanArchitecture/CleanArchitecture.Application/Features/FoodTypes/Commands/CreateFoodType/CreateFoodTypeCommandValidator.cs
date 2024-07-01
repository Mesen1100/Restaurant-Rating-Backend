using CleanArchitecture.Core.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.FoodTypes.Commands.CreateFoodType
{
    public class CreateFoodTypeCommandValidator :AbstractValidator<CreateFoodTypeCommand>
    {
        private readonly IFoodTypeRepositoryAsync _FoodTypeRepository;

        public CreateFoodTypeCommandValidator(IFoodTypeRepositoryAsync FoodTypeRepository)
        {
            this._FoodTypeRepository = FoodTypeRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} can't be null.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
