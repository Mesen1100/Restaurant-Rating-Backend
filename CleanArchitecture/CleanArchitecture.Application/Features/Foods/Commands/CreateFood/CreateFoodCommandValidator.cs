using CleanArchitecture.Infrastructure.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Foods.Commands.CreateFood
{
    public class CreateFoodCommandValidator:AbstractValidator<CreateFoodCommand>
    {
        private readonly IFoodRepositoryAsync _foodRepositoryAsync;

        public CreateFoodCommandValidator(IFoodRepositoryAsync foodRepositoryAsync)
        {
            this._foodRepositoryAsync = foodRepositoryAsync;

            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull().WithMessage("{PropertyName} can't be null.")
               .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");
            RuleFor(p => p.Price)
                .GreaterThan(0);
            RuleFor(p => p.MenuId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} can't be null.");

            RuleFor(p => p.FoodTypeId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} can't be null.");
        }
    }
}
