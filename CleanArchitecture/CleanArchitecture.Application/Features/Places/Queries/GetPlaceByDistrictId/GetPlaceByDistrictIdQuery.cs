using CleanArchitecture.Core.Features.Places.Queries.GetAllPlaces;
using CleanArchitecture.Core.Features.Places.Queries.GetPlaceByDistrictId;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Places.Queries.GetPlacesByDistrictId
{
    public class GetPlaceByDistrictIdQuery : IRequest<PagedResponse<IEnumerable<GetAllPlacesViewModel>>>
    {
        public int DistrictId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetPlaceByIdQueryHandler : IRequestHandler<GetPlaceByDistrictIdQuery, PagedResponse<IEnumerable<GetAllPlacesViewModel>>>
    {
        private readonly IPlaceRepositoryAsync _placeRepositoryAsync;

        public GetPlaceByIdQueryHandler(IPlaceRepositoryAsync placeRepositoryAsync)
        {
            _placeRepositoryAsync = placeRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> Handle(GetPlaceByDistrictIdQuery request, CancellationToken cancellationToken)
        {
            var validfilter = new GetPlaceByDistrictIdParameter
            {
                DistrictId = request.DistrictId,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            };
            return await _placeRepositoryAsync.GetPlaceByDistrictId(validfilter);
        }
    }
}
