using CleanArchitecture.Core.Features.Foods.Queries.GetAllFoods;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Foods.Queries.GetFoodsByName
{
    public class GetFoodsByNameQuery:IRequest<PagedResponse<IEnumerable<GetAllFoodsViewModel>>>
    {
        public string SearchString { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetFoodsByNameQueryHandler : IRequestHandler<GetFoodsByNameQuery, PagedResponse<IEnumerable<GetAllFoodsViewModel>>>
    {
        private readonly IFoodRepositoryAsync _foodRepositoryAsync;

        public GetFoodsByNameQueryHandler(IFoodRepositoryAsync foodRepositoryAsync)
        {
            _foodRepositoryAsync = foodRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetAllFoodsViewModel>>> Handle(GetFoodsByNameQuery request, CancellationToken cancellationToken)
        {
            var validfilter = new GetFoodsByNameParameter
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchString = request.SearchString
            };
            return await _foodRepositoryAsync.GetFoodByName(validfilter);
        }
    }
}
