using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Districts.Queries.GetDistrictByCityId
{
    public class GetDistrictByCityIdQuery:IRequest<PagedResponse<IEnumerable<GetDistrictByCityIdViewModel>>>
    {
        public int CityId { get; set; }
    }
    public class GetDistrictByCityIdQueryHandler:IRequestHandler<GetDistrictByCityIdQuery, PagedResponse<IEnumerable<GetDistrictByCityIdViewModel>>>
    {
        private readonly IDistrictRepositoryAsync _districtRepositoryAsync;
        private readonly ICityRepositoryAsync _cityRepositoryAsync;

        public GetDistrictByCityIdQueryHandler(IDistrictRepositoryAsync districtRepositoryAsync, ICityRepositoryAsync cityRepositoryAsync)
        {
            _districtRepositoryAsync = districtRepositoryAsync;
            _cityRepositoryAsync = cityRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetDistrictByCityIdViewModel>>> Handle(GetDistrictByCityIdQuery request, CancellationToken cancellationToken)
        {
            var city=await _cityRepositoryAsync.GetByIdAsync(request.CityId);
            if (city == null)
            {
                throw new EntityNotFoundException("City", request.CityId);
            }
            return await _districtRepositoryAsync.GetDistrictByCityId(request.CityId);

        }
    }
}
