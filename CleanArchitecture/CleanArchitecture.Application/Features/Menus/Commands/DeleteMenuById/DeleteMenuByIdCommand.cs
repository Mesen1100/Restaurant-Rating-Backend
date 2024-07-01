using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Menus.Commands.DeleteMenuById
{
    public class DeleteMenuByIdCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string ManagerUserId { get; set; }
    }
    public class DeleteMenuByIdCommandHandler : IRequestHandler<DeleteMenuByIdCommand, Response<int>>
    {
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;

        public DeleteMenuByIdCommandHandler(IMenuRepositoryAsync menuRepositoryAsync)
        {
            _menuRepositoryAsync = menuRepositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteMenuByIdCommand request, CancellationToken cancellationToken)
        {
            var menu = await _menuRepositoryAsync.GetByIdAsync(request.Id);
            if (menu == null) { throw new EntityNotFoundException("Menu", request.Id); }
            //TODO: Control the place userId is the same as UserId inside request
            menu.IsEnabled = false;
            await _menuRepositoryAsync.DeleteAsync(menu);
            return new Response<int>(menu.Id);
        }
    }
}
