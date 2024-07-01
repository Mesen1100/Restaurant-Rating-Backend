using CleanArchitecture.Core.Features.Menus.Queries.GetAllMenus;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Menus.Queries.GetMenuByPlaceId
{
    public class GetMenuByPlaceIdQuery:IRequest<PagedResponse<IEnumerable<GetAllMenusViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PlaceId { get; set; }
        public int MenuTypeId { get; set; }
    }
    public class GetMenuByPlaceIdQueryHandler : IRequestHandler<GetMenuByPlaceIdQuery, PagedResponse<IEnumerable<GetAllMenusViewModel>>>
    {
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;

        public GetMenuByPlaceIdQueryHandler(IMenuRepositoryAsync menuRepositoryAsync)
        {
            _menuRepositoryAsync = menuRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetAllMenusViewModel>>> Handle(GetMenuByPlaceIdQuery request, CancellationToken cancellationToken)
        {
            var validfilter = new GetMenuByPlaceIdParameter
            {
                PlaceId = request.PlaceId,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                MenuTypeId=request.MenuTypeId,
            };
            return await _menuRepositoryAsync.GetMenuByPlaceId(validfilter);
        }
    }
}
