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

namespace CleanArchitecture.Core.Features.PlaceRates.Commands.CreatePlaceRate
{
    public class CreatePlaceRateCommand:IRequest<Response<int>>
    {
        public double ServiceRate { get; set; }
        public double HygieneRate { get; set; }
        public int PlaceId { get; set; }
        public string UserId { get; set; }
    }
    public class CreatePlaceCommandHandler : IRequestHandler<CreatePlaceRateCommand, Response<int>>
    {
        private readonly IPlaceRateRepositoryAsync _placeRateRepositoryAsync;
        private readonly IPlaceRepositoryAsync _placeRepositoryAsync;
        public CreatePlaceCommandHandler(IPlaceRateRepositoryAsync placeRateRepositoryAsync, IPlaceRepositoryAsync placeRepositoryAsync)
        {
            _placeRateRepositoryAsync = placeRateRepositoryAsync;
            _placeRepositoryAsync = placeRepositoryAsync;
        }

        public async Task<Response<int>> Handle(CreatePlaceRateCommand request, CancellationToken cancellationToken)
        {
            var place =await _placeRepositoryAsync.GetPlaceById(request.PlaceId);
            if(place == null) { throw new EntityNotFoundException("Place", request.PlaceId); }
            var placeRate = new PlaceRate
            {
                ServiceRate = request.ServiceRate,
                HygieneRate = request.HygieneRate,
                PlaceId = request.PlaceId,
                UserId = request.UserId,
                RatePoint = (request.ServiceRate + request.HygieneRate) / 2,
                IsEnabled=true,
                IsShowned=true,
            };
            await _placeRateRepositoryAsync.AddAsync(placeRate);
            Place newPlace = _placeRepositoryAsync.RateUpdateAsync(placeRate);
            await _placeRepositoryAsync.UpdateAsync(newPlace);
            return new Response<int>(placeRate.Id);
        }
    }
}
