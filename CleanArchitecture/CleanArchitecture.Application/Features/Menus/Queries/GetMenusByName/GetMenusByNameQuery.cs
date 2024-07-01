using CleanArchitecture.Core.Features.Menus.Queries.GetAllMenus;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Menus.Queries.GetMenusByName
{
    public class GetMenusByNameQuery:IRequest<PagedResponse<IEnumerable<GetAllMenusViewModel>>>
    {
        public string SearchString { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
    public class GetMenusByNameQueryHandler : IRequestHandler<GetMenusByNameQuery, PagedResponse<IEnumerable<GetAllMenusViewModel>>>
    {
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;

        public GetMenusByNameQueryHandler(IMenuRepositoryAsync menuRepositoryAsync)
        {
            _menuRepositoryAsync = menuRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetAllMenusViewModel>>> Handle(GetMenusByNameQuery request, CancellationToken cancellationToken)
        {
            var validfilter = new GetMenusByNameParameter
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchString = request.SearchString
            };
            return await _menuRepositoryAsync.GetMenusByName(validfilter);
        }
    }
}
