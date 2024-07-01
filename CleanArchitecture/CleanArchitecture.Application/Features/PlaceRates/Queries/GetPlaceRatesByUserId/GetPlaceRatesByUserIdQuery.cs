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
    public class GetPlaceRatesByUserIdQuery:IRequest<PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllPlaceRatesByUserIdQueryHandler : IRequestHandler<GetPlaceRatesByUserIdQuery, PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>>
    {
        private readonly IPlaceRateRepositoryAsync _placeRateRepositoryAsync;

        public GetAllPlaceRatesByUserIdQueryHandler(IPlaceRateRepositoryAsync placeRateRepositoryAsync)
        {
            _placeRateRepositoryAsync = placeRateRepositoryAsync;
        }

        Task<PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>> IRequestHandler<GetPlaceRatesByUserIdQuery, PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>>.Handle(GetPlaceRatesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var validfiler = new GetAllPlaceRatesParameter
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            };
            return _placeRateRepositoryAsync.GetPlaceByUserId(request.UserId, validfiler);
            
        }
    }
}
