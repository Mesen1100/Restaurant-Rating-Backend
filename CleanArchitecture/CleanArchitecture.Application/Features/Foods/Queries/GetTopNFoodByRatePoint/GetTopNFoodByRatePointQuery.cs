using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Foods.Queries.GetTopNFoodByRatePoint
{
    public class GetTopNFoodByRatePointQuery:IRequest<Response<IEnumerable<GetTopNFoodByRatePointViewModel>>>
    {
        public int N {  get; set; }
        public bool direction { get; set; }
    }
    public class GetTopNFoodByRatePointQueryHandler : IRequestHandler<GetTopNFoodByRatePointQuery, Response<IEnumerable<GetTopNFoodByRatePointViewModel>>>
    {
        private readonly IFoodRepositoryAsync _foodRepositoryAsync;

        public GetTopNFoodByRatePointQueryHandler(IFoodRepositoryAsync foodRepositoryAsync)
        {
            _foodRepositoryAsync = foodRepositoryAsync;
        }

        public Task<Response<IEnumerable<GetTopNFoodByRatePointViewModel>>> Handle(GetTopNFoodByRatePointQuery request, CancellationToken cancellationToken)
        {
            return _foodRepositoryAsync.GetTopNFoodByRatePoint(request.N, request.direction);
        }
    }
}
