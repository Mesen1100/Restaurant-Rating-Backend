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

namespace CleanArchitecture.Core.Features.Districts.Commands
{
    public class CreateDistrictCommand:IRequest<Response<int>>
    {
        public string Name { get; set; }
        public int CityId { get; set; }
    }
    public class CreateDistrictCommandHandler : IRequestHandler<CreateDistrictCommand, Response<int>>
    {
        private readonly IDistrictRepositoryAsync _districtRepositoryAsync;
        private readonly ICityRepositoryAsync _cityRepositoryAsync;
        public CreateDistrictCommandHandler(IDistrictRepositoryAsync districtRepositoryAsync, ICityRepositoryAsync cityRepositoryAsync)
        {
            _districtRepositoryAsync = districtRepositoryAsync;
            _cityRepositoryAsync = cityRepositoryAsync;
        }

        public async Task<Response<int>> Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
        {
            var city = await _cityRepositoryAsync.GetByIdAsync(request.CityId);
            if(city == null)
            {
                throw new EntityNotFoundException("City", request.CityId);
            }
            var district = new District
            {
                CityId = request.CityId,
                Name = request.Name
            };
            await _districtRepositoryAsync.AddAsync(district);
            return new Response<int>(district.Id);
        }
    }
}
