using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.FoodRates.Commands.CreateFoodRate
{
    public class CreateFoodRateValidator:AbstractValidator<CreateFoodRateCommand>
    {
        private readonly IFoodRateRepositoryAsync _foodRateRepositoryAsync;

        public CreateFoodRateValidator(IFoodRateRepositoryAsync foodRateRepositoryAsync)
        {
            this._foodRateRepositoryAsync = foodRateRepositoryAsync;

            RuleFor(p=>p.TasteRate)
                .GreaterThanOrEqualTo(0).WithMessage("Taste Rate Can't be Lower Than 0")
                .LessThanOrEqualTo(10).WithMessage("Taste Rate Can't be Higher Than 10");

            RuleFor(p=>p.PriceRate)
                .GreaterThanOrEqualTo(0).WithMessage("Price Rate Can't be Lower Than 0")
                .LessThanOrEqualTo(10).WithMessage("Price Rate Can't be Higher Than 10");

            RuleFor(p => p.FoodId)
                .NotEmpty().WithMessage("Rate Need to Belong a Food");
        }
    }
}
