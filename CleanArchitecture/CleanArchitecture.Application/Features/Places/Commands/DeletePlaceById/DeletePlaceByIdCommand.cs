using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Places.Commands.DeletePlaceById
{
    public class DeletePlaceByIdCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string ManagerUserId { get; set; }
    }
    public class DeletePlaceByIdCommandHandler : IRequestHandler<DeletePlaceByIdCommand, Response<int>>
    {
        private readonly IPlaceRepositoryAsync _placeRepositoryAsync;

        public DeletePlaceByIdCommandHandler(IPlaceRepositoryAsync placeRepositoryAsync)
        {
            _placeRepositoryAsync = placeRepositoryAsync;
        }

        public async Task<Response<int>> Handle(DeletePlaceByIdCommand request, CancellationToken cancellationToken)
        {
            var place = await _placeRepositoryAsync.GetByIdAsync(request.Id);
            if (place == null) { throw new EntityNotFoundException("Place", place.Id); }
            if(place.ManagerUserId!=request.ManagerUserId) { throw new UserNotAuthorityException(place.User.Username); }
            await _placeRepositoryAsync.DeleteAsync(place);
            return new Response<int>(place.Id);
        }
    }
}
