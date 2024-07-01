using CleanArchitecture.Core.Features.Places.Queries.GetAllPlaces;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Places.Queries.GetPlaceById
{
    public class GetPlaceByIdQuery:IRequest<Response<GetAllPlacesViewModel>>
    {
        public int Id { get; set; }
    }
    public class GetPlaceByIdQueryHandler : IRequestHandler<GetPlaceByIdQuery, Response<GetAllPlacesViewModel>>
    {
        private readonly IPlaceRepositoryAsync _placeRepositoryAsync;

        public GetPlaceByIdQueryHandler(IPlaceRepositoryAsync placeRepositoryAsync)
        {
            _placeRepositoryAsync = placeRepositoryAsync;
        }

        public Task<Response<GetAllPlacesViewModel>> Handle(GetPlaceByIdQuery request, CancellationToken cancellationToken)
        {
            return _placeRepositoryAsync.GetPlaceById(request.Id);
        }
    }
}
