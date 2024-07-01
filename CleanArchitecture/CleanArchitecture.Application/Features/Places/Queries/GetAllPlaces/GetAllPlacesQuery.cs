using AutoMapper;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Places.Queries.GetAllPlaces
{
    public class GetAllPlacesQuery:IRequest<PagedResponse<IEnumerable<GetAllPlacesViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllPlacesQueryHandler:IRequestHandler<GetAllPlacesQuery, PagedResponse<IEnumerable<GetAllPlacesViewModel>>>
    {
        private readonly IPlaceRepositoryAsync _placeRepositoryAsync;
        private readonly IMapper _mapper;

        public GetAllPlacesQueryHandler(IPlaceRepositoryAsync placeRepositoryAsync, IMapper mapper)
        {
            _placeRepositoryAsync = placeRepositoryAsync;
            _mapper = mapper;
        }

        public Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> Handle(GetAllPlacesQuery request, CancellationToken cancellationToken)
        {
            var validfilter = new GetAllPlacesParameter
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            };
            return _placeRepositoryAsync.GetAllPlaces(validfilter);
        }
    }
}
