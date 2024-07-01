using AutoMapper;
using CleanArchitecture.Core.Features.Places.Queries.GetAllPlaces;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.PlaceRates.Queries.GetAllPlaceRates
{
    public class GetAllPlaceRatesQuery:IRequest<PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
    public class GetAllPlaceRatesQueryHandler:IRequestHandler<GetAllPlaceRatesQuery, PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>>
    {
        private readonly IPlaceRateRepositoryAsync _placeRateRepositoryAsync;
        private readonly IMapper _mapper;

        public GetAllPlaceRatesQueryHandler(IPlaceRateRepositoryAsync placeRateRepositoryAsync, IMapper mapper)
        {
            _placeRateRepositoryAsync = placeRateRepositoryAsync;
            _mapper = mapper;
        }

        public Task<PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>> Handle(GetAllPlaceRatesQuery request, CancellationToken cancellationToken)
        {
            var validfilter = new GetAllPlaceRatesParameter
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            };
            return _placeRateRepositoryAsync.GetAllPlaceRates(validfilter);
        }
    }
}
