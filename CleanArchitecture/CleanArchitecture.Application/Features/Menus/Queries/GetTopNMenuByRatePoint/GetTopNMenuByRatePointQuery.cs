using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Menus.Queries.GetTopNMenuByRatePoint
{
    public class GetTopNMenuByRatePointQuery:IRequest<Response<IEnumerable<GetTopNMenuByRatePointViewModel>>>
    {
        public int N { get; set; }
        public bool direction {  get; set; }
    }
    public class GetTopNMenuByRatePointQueryHandler : IRequestHandler<GetTopNMenuByRatePointQuery, Response<IEnumerable<GetTopNMenuByRatePointViewModel>>>
    {
        private IMenuRepositoryAsync _menuRepositoryAsync;

        public GetTopNMenuByRatePointQueryHandler(IMenuRepositoryAsync menuRepositoryAsync)
        {
            _menuRepositoryAsync = menuRepositoryAsync;
        }

        public Task<Response<IEnumerable<GetTopNMenuByRatePointViewModel>>> Handle(GetTopNMenuByRatePointQuery request, CancellationToken cancellationToken)
        {
            return _menuRepositoryAsync.GetTopNMenuByRatePoint(request.N, request.direction);
        }
    }
}
