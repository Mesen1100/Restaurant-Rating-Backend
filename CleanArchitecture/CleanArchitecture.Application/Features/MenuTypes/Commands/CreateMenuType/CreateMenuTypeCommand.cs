using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.MenuTypes.Commands.CreateMenuType
{
    public class CreateMenuTypeCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
    }
    public class CreateMenuTypeCommandHandler : IRequestHandler<CreateMenuTypeCommand,Response<int>>
    {
        private readonly IMenuTypeRepositoryAsync _MenutypeRepository;
        public CreateMenuTypeCommandHandler(IMenuTypeRepositoryAsync MenutypeRepository)
        {
            _MenutypeRepository = MenutypeRepository;
        }

        public async Task<Response<int>> Handle(CreateMenuTypeCommand request, CancellationToken cancellationToken)
        {
            var MenuType = new MenuType
            {
                Name = request.Name,
            };
            await _MenutypeRepository.AddAsync(MenuType);
            return new Response<int>(MenuType.Id);
        }
    }
}
