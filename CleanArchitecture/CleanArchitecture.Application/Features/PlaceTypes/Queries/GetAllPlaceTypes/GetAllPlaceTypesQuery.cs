using AutoMapper;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.PlaceTypes.Queries.GetAllPlaceTypes
{
    public class GetAllPlaceTypesQuery: IRequest<PagedResponse<IEnumerable<GetAllPlaceTypesViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllPlaceTypesQueryHandler : IRequestHandler<GetAllPlaceTypesQuery, PagedResponse<IEnumerable<GetAllPlaceTypesViewModel>>>
    {
        private readonly IPlaceTypeRepositoryAsync _placeTypeRepositoryAsync;
        private readonly IMapper _mapper;

        public GetAllPlaceTypesQueryHandler(IPlaceTypeRepositoryAsync placeTypeRepositoryAsync,IMapper mapper)
        {
            _placeTypeRepositoryAsync = placeTypeRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllPlaceTypesViewModel>>> Handle(GetAllPlaceTypesQuery request, CancellationToken cancellationToken)
        {
            var validfilter = new GetAllPlaceTypesParameter
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            };
            var placeType =await _placeTypeRepositoryAsync.GetPagedReponseAsync(validfilter.PageNumber, validfilter.PageSize);
            var placeTypeViewModel = _mapper.Map<IEnumerable<GetAllPlaceTypesViewModel>>(placeType);
            return new PagedResponse<IEnumerable<GetAllPlaceTypesViewModel>>(placeTypeViewModel, validfilter.PageNumber, validfilter.PageSize);
        }
    }
}
