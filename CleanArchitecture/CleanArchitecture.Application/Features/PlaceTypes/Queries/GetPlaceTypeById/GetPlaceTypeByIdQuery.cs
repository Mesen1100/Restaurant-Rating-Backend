using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.PlaceTypes.Queries.GetPlaceTypeById
{
    public class GetPlaceTypeByIdQuery:IRequest<Response<PlaceType>>
    {
        public int Id { get; set; }
    }
    public class GetPlaceTypeByIdQueryHandler : IRequestHandler<GetPlaceTypeByIdQuery, Response<PlaceType>>
    {
        private readonly IPlaceTypeRepositoryAsync _placeTypeRepositoryAsync;

        public GetPlaceTypeByIdQueryHandler(IPlaceTypeRepositoryAsync placeTypeRepositoryAsync)
        {
            _placeTypeRepositoryAsync = placeTypeRepositoryAsync;
        }

        public async Task<Response<PlaceType>> Handle(GetPlaceTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var placeType = await _placeTypeRepositoryAsync.GetByIdAsync(request.Id);
            if (placeType == null) { throw new EntityNotFoundException("Place Type", request.Id); }
            return new Response<PlaceType>(placeType);
        }
    }
}
