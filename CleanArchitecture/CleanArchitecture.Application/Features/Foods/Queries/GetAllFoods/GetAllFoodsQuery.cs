using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Foods.Queries.GetAllFoods
{
    public class GetAllFoodsQuery:IRequest<PagedResponse<IEnumerable<GetAllFoodsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllFoodsQueryHandler : IRequestHandler<GetAllFoodsQuery, PagedResponse<IEnumerable<GetAllFoodsViewModel>>>
    {
        private readonly IFoodRepositoryAsync _foodRepositoryAsync;

        public GetAllFoodsQueryHandler(IFoodRepositoryAsync foodRepositoryAsync)
        {
            _foodRepositoryAsync = foodRepositoryAsync;
        }

        public Task<PagedResponse<IEnumerable<GetAllFoodsViewModel>>> Handle(GetAllFoodsQuery request, CancellationToken cancellationToken)
        {
            var validfilter = new GetAllFoodsParameter
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            };

            return _foodRepositoryAsync.GetAllFoods(validfilter);
        }
    }
}
