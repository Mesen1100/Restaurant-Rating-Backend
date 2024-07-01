using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Features.Cities.Queries.GetAllCities;
using CleanArchitecture.Core.Features.Districts.Queries.GetDistrictByCityId;
using CleanArchitecture.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IDistrictRepositoryAsync:IGenericRepositoryAsync<District>
    {
        Task<PagedResponse<IEnumerable<GetDistrictByCityIdViewModel>>> GetDistrictByCityId(int cityId);
    }
}
