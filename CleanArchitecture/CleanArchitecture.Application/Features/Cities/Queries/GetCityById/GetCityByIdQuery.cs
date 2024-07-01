using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Features.Cities.Queries.GetAllCities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Cities.Queries.GetCityById
{
    public class GetCityByIdQuery:IRequest<Response<GetAllCitiesViewModel>>
    {
        public int Id { get; set; }
    }
    public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, Response<GetAllCitiesViewModel>>
    {
        private readonly ICityRepositoryAsync _cityRepositoryAsync;

        public GetCityByIdQueryHandler(ICityRepositoryAsync cityRepositoryAsync)
        {
            _cityRepositoryAsync = cityRepositoryAsync;
        }

        public async Task<Response<GetAllCitiesViewModel>> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            var city = await _cityRepositoryAsync.GetByIdAsync(request.Id);
            if (city == null)
            {
                throw new EntityNotFoundException("City",request.Id);
            }
            var result = new GetAllCitiesViewModel
            {
                Id = city.Id,
                Name = city.Name,
            };
            return new Response<GetAllCitiesViewModel>(result);
        }
    }
}
