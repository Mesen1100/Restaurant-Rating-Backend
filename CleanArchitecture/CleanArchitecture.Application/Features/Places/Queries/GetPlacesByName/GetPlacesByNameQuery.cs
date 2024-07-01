using CleanArchitecture.Core.Features.Places.Queries.GetAllPlaces;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Places.Queries.GetPlacesByName
{
    public class GetPlacesByNameQuery:IRequest<PagedResponse<IEnumerable<GetAllPlacesViewModel>>>
    {
        public string SearchString { get; set; }
        public int CityId { get; set; }
        public int PlaceTypeId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetPlaceByNameQueryHandler : IRequestHandler<GetPlacesByNameQuery, PagedResponse<IEnumerable<GetAllPlacesViewModel>>>
    {
        private readonly IPlaceRepositoryAsync _placeRepositoryAsync;

        public GetPlaceByNameQueryHandler(IPlaceRepositoryAsync placeRepositoryAsync)
        {
            _placeRepositoryAsync = placeRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> Handle(GetPlacesByNameQuery request, CancellationToken cancellationToken)
        {
            var validfilter = new GetPlacesByNameParameter
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchString = request.SearchString,
                CityId=request.CityId,
                PlaceTypeId=request.PlaceTypeId
            };
            return await _placeRepositoryAsync.GetPlacesByName(validfilter);
        }
    }
}
