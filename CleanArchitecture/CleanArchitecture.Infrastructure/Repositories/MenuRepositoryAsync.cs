using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Features.Foods.Queries.GetAllFoods;
using CleanArchitecture.Core.Features.Menus.Queries.GetAllMenus;
using CleanArchitecture.Core.Features.Menus.Queries.GetMenuByPlaceId;
using CleanArchitecture.Core.Features.Menus.Queries.GetMenusByName;
using CleanArchitecture.Core.Features.Menus.Queries.GetTopNMenuByRatePoint;
using CleanArchitecture.Core.Features.Places.Queries.GetAllPlaces;
using CleanArchitecture.Core.Features.Places.Queries.GetTopNPlaceByRatePoint;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class MenuRepositoryAsync : GenericRepositoryAsync<Menu>, IMenuRepositoryAsync
    {
        private readonly DbSet<Menu> _menus;
        public MenuRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _menus = dbContext.Set<Menu>();
        }

        public async Task<PagedResponse<IEnumerable<GetAllMenusViewModel>>> GetAllMenus(GetAllMenusParameter parameter)
        {
            IQueryable<Menu> menus = _menus.Include(p => p.Place).Include(p => p.MenuType).AsQueryable();
            var totalRecords = await menus.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Menu", totalRecords);
            }
            menus = menus.Skip((parameter.PageNumber - 1) * parameter.PageSize).Take(parameter.PageSize);
            var result = await menus.Select(p => new GetAllMenusViewModel
            {
                Id = p.Id,
                Name = p.Name,
                MenuRate=p.MenuRate,
                MenuRateCount=p.MenuRateCount,
                Description = p.Description,
                PlaceName=p.Place.Name,
                MenuTypeName = p.MenuType.Name,
            }).ToListAsync();
            return new PagedResponse<IEnumerable<GetAllMenusViewModel>>(result, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<Response<GetAllMenusViewModel>> GetMenuById(int id)
        {
            var menu = await _menus.Include(p => p.Place).Include(p => p.MenuType).FirstOrDefaultAsync(p => p.Id == id);
            if (menu == null)
            {
                throw new EntityNotFoundException("Menu", id);
            }
            var result = new GetAllMenusViewModel
            {
                Id = menu.Id,
                Name = menu.Name,
                Description = menu.Description,
                MenuTypeName=menu.MenuType.Name,
                PlaceName=menu.Place.Name,
            };
            return new Response<GetAllMenusViewModel>(result);
        }
        public async Task<bool> CanManagerHandleFood(int MenuId,string UserId)
        {
            var menu = await _menus.Include(p => p.Place).FirstOrDefaultAsync(p => p.Id == MenuId);
            if (menu == null)
            {
                throw new EntityNotFoundException("Menu", MenuId);
            }
            if (menu.Place.ManagerUserId == UserId)
            {
                return true;
            }
            return false;
        }

        public Menu RateUpdateAsync(FoodRate foodRate)
        {
            var menu = _menus.FirstOrDefault(p => p.Id == foodRate.Food.MenuId);
            if (menu == null) { throw new EntityNotFoundException("Menu", menu.Id); }
            double oldRatePoint = menu.MenuRate* menu.MenuRateCount;
            double newRateCount = menu.MenuRateCount+ 1;
            double newRatePoint = (oldRatePoint + foodRate.RatePoint) / (newRateCount);
            menu.MenuRate= newRatePoint;
            menu.MenuRateCount = newRateCount;
            return menu;
        }

        public async Task<Response<IEnumerable<GetTopNMenuByRatePointViewModel>>> GetTopNMenuByRatePoint(int n, bool direction)
        {
            //direction for increase rate point or decrease rate point
            //true is decrease 
            //false is increase
            IQueryable<Menu> menus;

            if (direction)
            {
                // Decreasing order
                menus = _menus
                    .Include(p => p.Place)
                    .Include(p => p.MenuType)
                    .OrderByDescending(p => p.MenuRate)
                    .Take(n)
                    .AsQueryable();
            }
            else
            {
                // Increasing order
                menus = _menus
                    .Include(p => p.Place)
                    .Include(p => p.MenuType)
                    .OrderBy(p => p.MenuRate)
                    .Take(n)
                    .AsQueryable();
            }
            var totalRecords = await menus.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Menu", totalRecords);
            }
            var result = await menus.Select(p => new GetTopNMenuByRatePointViewModel
            {
                Id=p.Id,
                Name=p.Name,
                Description=p.Description,
                MenuRate=p.MenuRate,
                MenuTypeName=p.MenuType.Name,
                PlaceName=p.Place.Name,
            }).ToListAsync();
            return new Response<IEnumerable<GetTopNMenuByRatePointViewModel>>(result);

        }

        public async Task<PagedResponse<IEnumerable<GetAllMenusViewModel>>> GetMenusByName(GetMenusByNameParameter parameter)
        {
            IQueryable<Menu> menus= _menus.Include(p => p.Place).Include(p => p.MenuType).AsQueryable();
            if (!string.IsNullOrEmpty(parameter.SearchString))
            {
                menus = menus.Where(f => f.Name.Contains(parameter.SearchString));
            }
            var totalRecords = await menus.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Menu", totalRecords);
            }
            menus = menus.Skip((parameter.PageNumber - 1) * parameter.PageSize).Take(parameter.PageSize);

            var result = await menus.Select(p => new GetAllMenusViewModel
            {
                Id = p.Id,
                Name = p.Name,
                MenuRate = p.MenuRate,
                MenuRateCount = p.MenuRateCount,
                Description = p.Description,
                PlaceName = p.Place.Name,
                MenuTypeName = p.MenuType.Name,
            }).ToListAsync();
            return new PagedResponse<IEnumerable<GetAllMenusViewModel>>(result, parameter.PageNumber, parameter.PageSize);
        }
        public async Task<PagedResponse<IEnumerable<GetAllMenusViewModel>>> GetMenuByPlaceId(GetMenuByPlaceIdParameter parameter)
        {
            IQueryable<Menu> menus = _menus.Include(p => p.Place).Include(p => p.MenuType).AsQueryable();
            if (parameter.PlaceId!=0)
            {
                menus = menus.Where(f => f.PlaceId==parameter.PlaceId);
            }
            if (parameter.MenuTypeId != 0)
            {
                menus = menus.Where(f => f.MenuTypeId == parameter.MenuTypeId);
            }
            var totalRecords = await menus.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Menu", totalRecords);
            }
            menus = menus.Skip((parameter.PageNumber - 1) * parameter.PageSize).Take(parameter.PageSize);

            var result = await menus.Select(p => new GetAllMenusViewModel
            {
                Id = p.Id,
                Name = p.Name,
                MenuRate = p.MenuRate,
                MenuRateCount = p.MenuRateCount,
                Description = p.Description,
                PlaceName = p.Place.Name,
                MenuTypeName = p.MenuType.Name,
            }).ToListAsync();
            return new PagedResponse<IEnumerable<GetAllMenusViewModel>>(result, parameter.PageNumber, parameter.PageSize);
        }
    }
}
