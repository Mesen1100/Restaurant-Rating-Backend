using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.PlaceTypes.Commands.DeletePlaceTypeById
{
    public class DeletePlaceTypeByIdCommand :IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeletePlaceTypeByIdCommandHandler : IRequestHandler<DeletePlaceTypeByIdCommand, Response<int>>
    {
        private readonly IPlaceTypeRepositoryAsync _placeTypeRepositoryAsync;

        public DeletePlaceTypeByIdCommandHandler(IPlaceTypeRepositoryAsync placeTypeRepositoryAsync)
        {
            _placeTypeRepositoryAsync = placeTypeRepositoryAsync;
        }

        public async Task<Response<int>> Handle(DeletePlaceTypeByIdCommand request, CancellationToken cancellationToken)
        {
            var placeType = await _placeTypeRepositoryAsync.GetByIdAsync(request.Id);
            if (placeType == null) { throw new EntityNotFoundException("Place Type",request.Id); }
            await _placeTypeRepositoryAsync.DeleteAsync(placeType);
            return new Response<int>(placeType.Id);

        }
    }
}
