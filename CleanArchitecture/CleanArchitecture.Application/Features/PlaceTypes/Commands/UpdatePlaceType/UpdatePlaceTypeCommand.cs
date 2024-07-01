using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.PlaceTypes.Commands.UpdatePlaceType
{
    public class UpdatePlaceTypeCommand :IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdatePlaceTypeCommandHandler : IRequestHandler<UpdatePlaceTypeCommand, Response<int>>
    {
        private readonly IPlaceTypeRepositoryAsync _placeTypeRepositoryAsync;
        public UpdatePlaceTypeCommandHandler(IPlaceTypeRepositoryAsync placeTypeRepositoryAsync)
        {
            _placeTypeRepositoryAsync = placeTypeRepositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdatePlaceTypeCommand request, CancellationToken cancellationToken)
        {
            var placeType = await _placeTypeRepositoryAsync.GetByIdAsync(request.Id);
            if(placeType==null) { throw new EntityNotFoundException("Place Type", request.Id); }
            placeType.Name = request.Name;
            await _placeTypeRepositoryAsync.UpdateAsync(placeType);
            return new Response<int>(placeType.Id);
        }
    }
}
