using CleanArchitecture.Core.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Features.MenuTypes.Commands.CreateMenuType
{
    public class CreateMenuTypeCommandValidator :AbstractValidator<CreateMenuTypeCommand>
    {
        private readonly IMenuTypeRepositoryAsync _MenuTypeRepository;

        public CreateMenuTypeCommandValidator(IMenuTypeRepositoryAsync MenuTypeRepository)
        {
            this._MenuTypeRepository = MenuTypeRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} can't be null.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
