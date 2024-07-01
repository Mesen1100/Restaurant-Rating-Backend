using CleanArchitecture.Core.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.Menus.Commands.CreateMenu
{
    public class CreateMenuCommandValidator:AbstractValidator<CreateMenuCommand>
    {
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;

        public CreateMenuCommandValidator(IMenuRepositoryAsync menuRepositoryAsync)
        {
            this._menuRepositoryAsync = menuRepositoryAsync;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} can't be null.")
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");

            RuleFor(p => p.PlaceId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} can't be null.");

            RuleFor(p => p.MenuTypeId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} can't be null.");
        }
    }
}
