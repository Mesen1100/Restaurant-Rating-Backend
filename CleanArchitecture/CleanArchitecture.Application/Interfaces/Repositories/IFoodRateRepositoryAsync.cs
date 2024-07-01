using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Features.FoodRates.Queries.GetAllFoodRates;
using CleanArchitecture.Core.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IFoodRateRepositoryAsync:IGenericRepositoryAsync<FoodRate>
    {
        Task<PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>> GetAllFoodRates(GetAllFoodRatesParameter parameter);
        Task<Response<GetAllFoodRatesViewModel>> GetFoodRateById(int id);
        Task<PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>> GetFoodRateByUserId(string userId, GetAllFoodRatesParameter parameter);
    }
}
