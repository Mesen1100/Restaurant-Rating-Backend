using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Menus.Commands.UpdateMenu
{
    public class UpdateMenuCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MenuTypeId { get; set; }
        public string ManagerUserId { get; set; }
    }
    public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, Response<int>>
    {
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;

        public UpdateMenuCommandHandler(IMenuRepositoryAsync menuRepositoryAsync)
        {
            _menuRepositoryAsync = menuRepositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _menuRepositoryAsync.GetByIdAsync(request.Id);
            if (menu == null) { throw new EntityNotFoundException("Menu", request.Id); }
            //TODO: Manager UserId checked here
            menu.Name = request.Name;
            menu.Description = request.Description;
            menu.MenuTypeId = request.MenuTypeId;
            //TODO: Check menu typeId is null
            return new Response<int>(menu.Id);
        }
    }
}
