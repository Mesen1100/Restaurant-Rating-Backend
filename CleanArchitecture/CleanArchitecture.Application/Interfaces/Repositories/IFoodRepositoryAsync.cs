using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Features.Foods.Queries.GetAllFoods;
using CleanArchitecture.Core.Features.Foods.Queries.GetFoodsByMenuId;
using CleanArchitecture.Core.Features.Foods.Queries.GetFoodsByName;
using CleanArchitecture.Core.Features.Foods.Queries.GetTopNFoodByRatePoint;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public interface IFoodRepositoryAsync:IGenericRepositoryAsync<Food>
    {
        Task<PagedResponse<IEnumerable<GetAllFoodsViewModel>>> GetAllFoods(GetAllFoodsParameter parameter);
        Task<Response<GetAllFoodsViewModel>> GetFoodById(int id);
        Food RateUpdateAsync(FoodRate foodRate);
        Task<Response<IEnumerable<GetTopNFoodByRatePointViewModel>>> GetTopNFoodByRatePoint(int n, bool direction);
        Task<PagedResponse<IEnumerable<GetAllFoodsViewModel>>> GetFoodByName(GetFoodsByNameParameter parameter);
        Task<PagedResponse<IEnumerable<GetAllFoodsViewModel>>> GetFoodByParameter(GetFoodByParameterParameter parameter);
    }
}