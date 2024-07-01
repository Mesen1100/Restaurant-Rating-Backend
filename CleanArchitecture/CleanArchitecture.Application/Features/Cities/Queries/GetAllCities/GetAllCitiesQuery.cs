using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Cities.Queries.GetAllCities
{
    public class GetAllCitiesQuery:IRequest<PagedResponse<IEnumerable<GetAllCitiesViewModel>>>
    {

    }
    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, PagedResponse<IEnumerable<GetAllCitiesViewModel>>>
    {
        private readonly ICityRepositoryAsync _cityRepositoryAsync;

        public GetAllCitiesQueryHandler(ICityRepositoryAsync cityRepositoryAsync)
        {
            _cityRepositoryAsync = cityRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetAllCitiesViewModel>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var result =await _cityRepositoryAsync.GetAllCities();
            return result;
        }
    }
}
