using CleanArchitecture.Core.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Places.Commands.CreatePlace
{
    public class CreatePlaceCommandValidator:AbstractValidator<CreatePlaceCommand>
    {
        private readonly IPlaceRepositoryAsync _placeRepositoyAsync;

        public CreatePlaceCommandValidator(IPlaceRepositoryAsync placeRepositoyAsync)
        {
            this._placeRepositoyAsync = placeRepositoyAsync;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} can't be null.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 200 characters.");
            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} can't be null.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 200 characters.");
            RuleFor(p => p.CityId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} can't be null.");
            RuleFor(p => p.DistrictId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} can't be null.");
            RuleFor(p => p.PlaceTypeId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} can't be null.");
            RuleFor(p => p.ManagerUserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} can't be null.");

        }
    }
}
