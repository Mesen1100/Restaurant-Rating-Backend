using CleanArchitecture.Core.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.PlaceRates.Commands.CreatePlaceRate
{
    public class CreatePlaceRateValidator:AbstractValidator<CreatePlaceRateCommand>
    {
        private readonly IPlaceRateRepositoryAsync _placeRateRepositoryAsync;

        public CreatePlaceRateValidator(IPlaceRateRepositoryAsync placeRateRepositoryAsync)
        {
            this._placeRateRepositoryAsync = placeRateRepositoryAsync;

            RuleFor(p => p.ServiceRate)
                .GreaterThanOrEqualTo(0).WithMessage("Service Rate Can't be Lower Than 0")
                .LessThanOrEqualTo(10).WithMessage("Service Rate Can't be Higher Than 10");

            RuleFor(p => p.HygieneRate)
                .GreaterThanOrEqualTo(0).WithMessage("Hygiene Rate Can't be Lower Than 0")
                .LessThanOrEqualTo(10).WithMessage("Hygiene Rate Can't be Higher Than 10");

            RuleFor(p => p.PlaceId)
                .NotEmpty().WithMessage("Rate Need to Belong a Place");
        }
    }
}
