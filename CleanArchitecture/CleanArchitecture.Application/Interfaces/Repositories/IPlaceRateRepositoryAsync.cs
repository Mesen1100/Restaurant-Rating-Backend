using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Features.PlaceRates.Queries.GetAllPlaceRates;
using CleanArchitecture.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IPlaceRateRepositoryAsync:IGenericRepositoryAsync<PlaceRate>
    {
        Task<PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>> GetAllPlaceRates(GetAllPlaceRatesParameter parameter);
        Task<Response<GetAllPlaceRatesViewModel>> GetPlaceRateById(int id);
        Task<PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>> GetPlaceByUserId(string userId, GetAllPlaceRatesParameter parameter);
    }
}
