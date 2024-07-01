using CleanArchitecture.Core.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.PlaceTypes.Commands.CreatePlaceType
{
    public class CreatePlaceTypeCommandValidator :AbstractValidator<CreatePlaceTypeCommand>
    {
        private readonly IPlaceTypeRepositoryAsync _placeTypeRepository;

        public CreatePlaceTypeCommandValidator(IPlaceTypeRepositoryAsync placeTypeRepository)
        {
            this._placeTypeRepository = placeTypeRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} can't be null.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
