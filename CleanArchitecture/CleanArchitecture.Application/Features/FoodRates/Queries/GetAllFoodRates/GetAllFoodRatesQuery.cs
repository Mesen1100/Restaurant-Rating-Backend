using AutoMapper;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.FoodRates.Queries.GetAllFoodRates
{
    public class GetAllFoodRatesQuery:IRequest<PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
    public class GetAllFoodRatesQueryHandler:IRequestHandler<GetAllFoodRatesQuery, PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>>
    {
        private readonly IFoodRateRepositoryAsync _foodRateRepositoryAsync;
        private readonly IMapper _mapper;

        public GetAllFoodRatesQueryHandler(IFoodRateRepositoryAsync foodRateRepositoryAsync, IMapper mapper)
        {
            _foodRateRepositoryAsync = foodRateRepositoryAsync;
            _mapper = mapper;
        }

        public Task<PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>> Handle(GetAllFoodRatesQuery request, CancellationToken cancellationToken)
        {
            var validfilter = new GetAllFoodRatesParameter
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            };
            return _foodRateRepositoryAsync.GetAllFoodRates(validfilter);
        }
    }
}
