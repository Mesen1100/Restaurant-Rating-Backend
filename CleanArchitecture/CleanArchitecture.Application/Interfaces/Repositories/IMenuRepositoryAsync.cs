using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Features.Menus.Queries.GetAllMenus;
using CleanArchitecture.Core.Features.Menus.Queries.GetMenuByPlaceId;
using CleanArchitecture.Core.Features.Menus.Queries.GetMenusByName;
using CleanArchitecture.Core.Features.Menus.Queries.GetTopNMenuByRatePoint;
using CleanArchitecture.Core.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IMenuRepositoryAsync:IGenericRepositoryAsync<Menu>
    {
        Task<bool> CanManagerHandleFood(int MenuId, string UserId);
        Task<PagedResponse<IEnumerable<GetAllMenusViewModel>>> GetAllMenus(GetAllMenusParameter parameter);
        Task<Response<GetAllMenusViewModel>> GetMenuById(int id);
        Menu RateUpdateAsync(FoodRate foodRate);
        Task<Response<IEnumerable<GetTopNMenuByRatePointViewModel>>> GetTopNMenuByRatePoint(int n, bool direction);
        Task<PagedResponse<IEnumerable<GetAllMenusViewModel>>> GetMenusByName(GetMenusByNameParameter parameter);
        Task<PagedResponse<IEnumerable<GetAllMenusViewModel>>> GetMenuByPlaceId(GetMenuByPlaceIdParameter parameter);
    }
}
