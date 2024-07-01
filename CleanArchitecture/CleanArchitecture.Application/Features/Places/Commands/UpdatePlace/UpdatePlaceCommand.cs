using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Places.Commands.UpdatePlace
{
    public class UpdatePlaceCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int PlaceTypeId { get; set; }
        public string ManagerUserId { get; set; }
    }
    public class UpdatePlaceCommandHandler : IRequestHandler<UpdatePlaceCommand, Response<int>>
    {
        private readonly IPlaceRepositoryAsync _placeRepositoryAsync;

        public UpdatePlaceCommandHandler(IPlaceRepositoryAsync placeRepositoryAsync)
        {
            _placeRepositoryAsync = placeRepositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdatePlaceCommand request, CancellationToken cancellationToken)
        {
            var place = await _placeRepositoryAsync.GetByIdAsync(request.Id);
            if(place==null) { throw new EntityNotFoundException("Place", request.Id); }
            if (place.ManagerUserId != request.ManagerUserId) { throw new UserNotAuthorityException(place.User.Username); }
            if (!string.IsNullOrEmpty(request.Name)) {
                place.Name = request.Name;
            }
            if (!string.IsNullOrEmpty(request.Description))
            {
                place.Description = request.Description;
            }
            
            


            //TODO:Check PlaceTypeId is null
            await _placeRepositoryAsync.UpdateAsync(place);
            return new Response<int>(place.Id);
        }
    }
}
