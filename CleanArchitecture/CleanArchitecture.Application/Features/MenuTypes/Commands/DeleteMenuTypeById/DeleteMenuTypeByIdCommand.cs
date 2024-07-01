using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.MenuTypes.Commands.DeleteMenuTypeById
{
    public class DeleteMenuTypeByIdCommand :IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteMenuTypeByIdCommandHandler : IRequestHandler<DeleteMenuTypeByIdCommand, Response<int>>
    {
        private readonly IMenuTypeRepositoryAsync _MenuTypeRepositoryAsync;

        public DeleteMenuTypeByIdCommandHandler(IMenuTypeRepositoryAsync MenuTypeRepositoryAsync)
        {
            _MenuTypeRepositoryAsync = MenuTypeRepositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteMenuTypeByIdCommand request, CancellationToken cancellationToken)
        {
            var MenuType = await _MenuTypeRepositoryAsync.GetByIdAsync(request.Id);
            if (MenuType == null) { throw new EntityNotFoundException("Menu Type",request.Id); }
            await _MenuTypeRepositoryAsync.DeleteAsync(MenuType);
            return new Response<int>(MenuType.Id);

        }
    }
}
