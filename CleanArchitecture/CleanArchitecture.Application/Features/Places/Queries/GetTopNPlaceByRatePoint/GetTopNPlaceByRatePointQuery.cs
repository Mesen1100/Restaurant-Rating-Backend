using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Places.Queries.GetTopNPlaceByRatePoint
{
    public class GetTopNPlaceByRatePointQuery:IRequest<Response<IEnumerable<GetTopNPlaceByRatePointViewModel>>>
    {
        public int N { get; set; }
        public bool Direction { get; set; }
    }
    public class GetTopNPlaceByRatePointQueryHandler : IRequestHandler<GetTopNPlaceByRatePointQuery, Response<IEnumerable<GetTopNPlaceByRatePointViewModel>>>
    {
        private readonly IPlaceRepositoryAsync _placeRepositoryAsync;

        public GetTopNPlaceByRatePointQueryHandler(IPlaceRepositoryAsync placeRepositoryAsync)
        {
            _placeRepositoryAsync = placeRepositoryAsync;
        }

        public Task<Response<IEnumerable<GetTopNPlaceByRatePointViewModel>>> Handle(GetTopNPlaceByRatePointQuery request, CancellationToken cancellationToken)
        {
            return _placeRepositoryAsync.GetTopNPlaceByRatePoint(request.N, request.Direction);
        }
    }
}
