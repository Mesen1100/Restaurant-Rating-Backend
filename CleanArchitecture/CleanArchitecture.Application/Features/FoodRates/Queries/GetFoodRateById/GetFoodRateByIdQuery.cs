using CleanArchitecture.Core.Features.FoodRates.Queries.GetAllFoodRates;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.PlaceRates.Queries.GetPlaceRateById
{
    public class GetFoodRateByIdQuery:IRequest<Response<GetAllFoodRatesViewModel>>
    {
        public int Id { get; set; }
    }
    public class GetAllFoodRateByIdQueryHandler : IRequestHandler<GetFoodRateByIdQuery, Response<GetAllFoodRatesViewModel>>
    {
        private readonly IFoodRateRepositoryAsync _foodRateRepositoryAsync;

        public GetAllFoodRateByIdQueryHandler(IFoodRateRepositoryAsync foodRateRepositoryAsync)
        {
            _foodRateRepositoryAsync = foodRateRepositoryAsync;
        }

        public Task<Response<GetAllFoodRatesViewModel>> Handle(GetFoodRateByIdQuery request, CancellationToken cancellationToken)
        {
            return _foodRateRepositoryAsync.GetFoodRateById(request.Id);
        }
    }
}
