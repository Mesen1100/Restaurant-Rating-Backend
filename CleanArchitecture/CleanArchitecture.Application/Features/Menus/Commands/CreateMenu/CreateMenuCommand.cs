using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Menus.Commands.CreateMenu
{
    public class CreateMenuCommand:IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PlaceId { get; set; }
        public int MenuTypeId { get; set; }
        public string ManagerUserId { get; set; }
    }
    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, Response<int>>
    {
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;

        public CreateMenuCommandHandler(IMenuRepositoryAsync menuRepositoryAsync)
        {
            _menuRepositoryAsync = menuRepositoryAsync;
        }

        public async Task<Response<int>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            //TODO:Check ManagerUserId is the place manager
            var menu = new Menu {
                Name = request.Name,
                Description = request.Description,
                MenuRate = 0,
                IsEnabled = false,
                IsShowned=false,
                PlaceId=request.PlaceId,
                MenuTypeId=request.MenuTypeId
            };
            await _menuRepositoryAsync.AddAsync(menu);
            return new Response<int>(menu.Id);
        }
    }
}
