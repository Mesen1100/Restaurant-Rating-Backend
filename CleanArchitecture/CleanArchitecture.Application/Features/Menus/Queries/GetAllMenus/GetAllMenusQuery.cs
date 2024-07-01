using AutoMapper;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Menus.Queries.GetAllMenus
{
    public class GetAllMenusQuery:IRequest<PagedResponse<IEnumerable<GetAllMenusViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, PagedResponse<IEnumerable<GetAllMenusViewModel>>>
    {
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;
        private readonly IMapper _mapper;

        public GetAllMenusQueryHandler(IMenuRepositoryAsync menuRepositoryAsync, IMapper mapper)
        {
            _menuRepositoryAsync = menuRepositoryAsync;
            _mapper = mapper;
        }

        public Task<PagedResponse<IEnumerable<GetAllMenusViewModel>>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            var validfilter = new GetAllMenusParameter
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            };

            return _menuRepositoryAsync.GetAllMenus(validfilter);
        }
    }
}
