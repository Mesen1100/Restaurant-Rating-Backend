using CleanArchitecture.Core.Features.Places.Queries.GetAllPlaces;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Places.Queries.GetPlacesByCityId
{
    public class GetPlaceByCityIdQuery:IRequest<PagedResponse<IEnumerable<GetAllPlacesViewModel>>>
    {
        public int CityId { get; set; }
        public int PlaceTypeId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetPlaceByIdQueryHandler : IRequestHandler<GetPlaceByCityIdQuery, PagedResponse<IEnumerable<GetAllPlacesViewModel>>>
    {
        private readonly IPlaceRepositoryAsync _placeRepositoryAsync;

        public GetPlaceByIdQueryHandler(IPlaceRepositoryAsync placeRepositoryAsync)
        {
            _placeRepositoryAsync = placeRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> Handle(GetPlaceByCityIdQuery request, CancellationToken cancellationToken)
        {
            var validfilter = new GetPlaceByCityIdParameter
            {
                CityId = request.CityId,
                PlaceTypeId= request.PlaceTypeId,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            };
            return await _placeRepositoryAsync.GetPlaceByCityId(validfilter);
        }
    }
}
