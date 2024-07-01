using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Places.Commands.CreatePlace
{
    public class CreatePlaceCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int PlaceTypeId { get; set; }
        public string ManagerUserId { get; set; }
    }
    public class CreatePlaceCommandHandler : IRequestHandler<CreatePlaceCommand, Response<int>>
    {
        private readonly IPlaceRepositoryAsync _placeRepositoryAsync;
        private readonly IUserRepositoryAsync _userRepositoryAsync;

        public CreatePlaceCommandHandler(IPlaceRepositoryAsync placeRepositoryAsync,IUserRepositoryAsync userRepositoryAsync)
        {
            _placeRepositoryAsync = placeRepositoryAsync;
            _userRepositoryAsync = userRepositoryAsync;
        }

        public async Task<Response<int>> Handle(CreatePlaceCommand request, CancellationToken cancellationToken)
        {
            var place = new Place
            {
                Name = request.Name,
                Description = request.Description,
                Address = request.Address,
                CityId = request.CityId,
                DistrictId = request.DistrictId,
                PlaceTypeId = request.PlaceTypeId,
                ManagerUserId = request.ManagerUserId,
            };
        
            await _placeRepositoryAsync.AddAsync(place);
            return new Response<int>(place.Id);
        }
    }
}
