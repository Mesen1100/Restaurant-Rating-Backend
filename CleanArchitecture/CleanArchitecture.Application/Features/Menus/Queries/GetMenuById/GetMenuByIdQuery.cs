using CleanArchitecture.Core.Features.Menus.Queries.GetAllMenus;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Menus.Queries.GetMenuById
{
    public class GetMenuByIdQuery:IRequest<Response<GetAllMenusViewModel>>
    {
        public int Id { get; set; }
    }
    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, Response<GetAllMenusViewModel>>
    {
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;

        public GetMenuByIdQueryHandler(IMenuRepositoryAsync menuRepositoryAsync)
        {
            _menuRepositoryAsync = menuRepositoryAsync;
        }

        public async Task<Response<GetAllMenusViewModel>> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            return await _menuRepositoryAsync.GetMenuById(request.Id);
        }
    }
}
