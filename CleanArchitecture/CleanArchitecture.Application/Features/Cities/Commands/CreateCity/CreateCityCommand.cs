using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommand:IRequest<Response<int>>
    {
        public string Name { get; set; }
    }
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, Response<int>>
    {
        private readonly ICityRepositoryAsync _cityRepositoryAsync;

        public CreateCityCommandHandler(ICityRepositoryAsync cityRepositoryAsync)
        {
            _cityRepositoryAsync = cityRepositoryAsync;
        }

        public async Task<Response<int>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var city = new City
            {
                Name = request.Name
            };
            await _cityRepositoryAsync.AddAsync(city);
            return new Response<int>(city.Id);
        }
    }
}
