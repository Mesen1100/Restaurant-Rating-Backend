using CleanArchitecture.Core.Features.FoodRates.Queries.GetAllFoodRates;
using CleanArchitecture.Core.Features.PlaceRates.Queries.GetAllPlaceRates;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.PlaceRates.Queries.GetPlaceRatesByUserId
{
    public class GetFoodRatesByUserIdQuery:IRequest<PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllFoodRatesByUserIdQueryHandler : IRequestHandler<GetFoodRatesByUserIdQuery, PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>>
    {
        private readonly IFoodRateRepositoryAsync _foodRateRepositoryAsync;

        public GetAllFoodRatesByUserIdQueryHandler(IFoodRateRepositoryAsync foodRateRepositoryAsync)
        {
            _foodRateRepositoryAsync = foodRateRepositoryAsync;
        }

        Task<PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>> IRequestHandler<GetFoodRatesByUserIdQuery, PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>>.Handle(GetFoodRatesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var validfiler = new GetAllFoodRatesParameter
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            };
            return _foodRateRepositoryAsync.GetFoodRateByUserId(request.UserId, validfiler);
            
        }
    }
}
