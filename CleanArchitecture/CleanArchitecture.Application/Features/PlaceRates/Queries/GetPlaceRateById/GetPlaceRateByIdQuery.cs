using CleanArchitecture.Core.Features.PlaceRates.Queries.GetAllPlaceRates;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.PlaceRates.Queries.GetPlaceRateById
{
    public class GetPlaceRateByIdQuery:IRequest<Response<GetAllPlaceRatesViewModel>>
    {
        public int Id { get; set; }
    }
    public class GetAllPlaceRateByIdQueryHandler : IRequestHandler<GetPlaceRateByIdQuery, Response<GetAllPlaceRatesViewModel>>
    {
        private readonly IPlaceRateRepositoryAsync _placeRateRepositoryAsync;

        public GetAllPlaceRateByIdQueryHandler(IPlaceRateRepositoryAsync placeRateRepositoryAsync)
        {
            _placeRateRepositoryAsync = placeRateRepositoryAsync;
        }

        public Task<Response<GetAllPlaceRatesViewModel>> Handle(GetPlaceRateByIdQuery request, CancellationToken cancellationToken)
        {
            return _placeRateRepositoryAsync.GetPlaceRateById(request.Id);
        }
    }
}
