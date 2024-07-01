using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Features.Foods.Queries.GetAllFoods;
using CleanArchitecture.Core.Features.Foods.Queries.GetFoodsByMenuId;
using CleanArchitecture.Core.Features.Foods.Queries.GetFoodsByName;
using CleanArchitecture.Core.Features.Foods.Queries.GetTopNFoodByRatePoint;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class FoodRepositoryAsync : GenericRepositoryAsync<Food>, IFoodRepositoryAsync
    {
        private readonly DbSet<Food> _foods;

        public FoodRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _foods = dbContext.Set<Food>();
        }
        public async Task<PagedResponse<IEnumerable<GetAllFoodsViewModel>>> GetAllFoods(GetAllFoodsParameter parameter)
        {
            IQueryable<Food> foods = _foods.Include(p => p.Menu).Include(p => p.FoodType).Include(p=>p.Menu.Place).AsQueryable();
            var totalRecords = await foods.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Food", totalRecords);
            }
            foods = foods.Skip((parameter.PageNumber - 1) * parameter.PageSize).Take(parameter.PageSize);
            var result = await foods.Select(p => new GetAllFoodsViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                FoodImage = p.FoodImage,
                Price = p.Price,
                CommentCount = p.CommentCount,
                IsEnabled = p.IsEnabled,
                IsShowned = p.IsShowned,
                MenuName = p.Menu.Name,
                RateCount = p.RateCount,
                RatePoint = p.RatePoint,
                FoodTypeName = p.FoodType.Name,
                PlaceName=p.Menu.Place.Name,
            }).ToListAsync();
            return new PagedResponse<IEnumerable<GetAllFoodsViewModel>>(result, parameter.PageNumber, parameter.PageSize);
        }
        public async Task<Response<GetAllFoodsViewModel>> GetFoodById(int id)
        {
            var food = await _foods.Include(p => p.Menu).Include(p => p.FoodType).Include(p=>p.Menu.Place).FirstOrDefaultAsync(p => p.Id == id);
            if (food == null)
            {
                throw new EntityNotFoundException("Food", id);
            }
            var result = new GetAllFoodsViewModel
            {
                Id = food.Id,
                Name = food.Name,
                Description = food.Description,
                FoodImage = food.FoodImage,
                Price = food.Price,
                CommentCount = food.CommentCount,
                IsEnabled = food.IsEnabled,
                IsShowned = food.IsShowned,
                MenuName = food.Menu.Name,
                RateCount = food.RateCount,
                RatePoint = food.RatePoint,
                FoodTypeName = food.FoodType.Name,
                PlaceName=food.Menu.Place.Name,
            };
            return new Response<GetAllFoodsViewModel>(result);
        }
        public Food RateUpdateAsync(FoodRate foodRate)
        {
            var food = _foods.FirstOrDefault(p => p.Id == foodRate.FoodId);
            if (food == null) { throw new EntityNotFoundException("Food", food.Id); }
            double oldRatePoint = food.RatePoint * food.RateCount;
            double newRateCount = food.RateCount + 1;
            double newRatePoint = (oldRatePoint + foodRate.RatePoint) / (newRateCount);
            food.RatePoint = newRatePoint;
            food.RateCount = newRateCount;
            return food;
        }
        public async Task<Response<IEnumerable<GetTopNFoodByRatePointViewModel>>> GetTopNFoodByRatePoint(int n, bool direction)
        {
            //direction for increase rate point or decrease rate point
            //true is decrease 
            //false is increase
            IQueryable<Food> foods;

            if (direction)
            {
                // Decreasing order
                foods = _foods
                    .Include(p => p.FoodType)
                    .OrderByDescending(p => p.RatePoint)
                    .Take(n)
                    .AsQueryable();
            }
            else
            {
                // Increasing order
                foods = _foods
                    .Include(p => p.FoodType)
                    .OrderBy(p => p.RatePoint)
                    .Take(n)
                    .AsQueryable();
            }
            var totalRecords = await foods.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Food", totalRecords);
            }
            var result = await foods.Select(p => new GetTopNFoodByRatePointViewModel
            {
                Id = p.Id,
                FoodImage = p.FoodImage,
                RatePoint = p.RatePoint,
                Name = p.Name,
                FoodTypeName = p.FoodType.Name,
                Price = p.Price,
            }).ToListAsync();
            return new Response<IEnumerable<GetTopNFoodByRatePointViewModel>>(result);
        }
        public async Task<PagedResponse<IEnumerable<GetAllFoodsViewModel>>> GetFoodByName(GetFoodsByNameParameter parameter)
        {
            IQueryable<Food> foods = _foods.Include(p => p.Menu).Include(p => p.FoodType).Include(p => p.Menu.Place).AsQueryable();
            if (!string.IsNullOrEmpty(parameter.SearchString))
            {
                foods = foods.Where(f => f.Name.Contains(parameter.SearchString));
            }
            var totalRecords = await foods.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Food", totalRecords);
            }
            foods = foods.Skip((parameter.PageNumber - 1) * parameter.PageSize).Take(parameter.PageSize);

            var result = await foods.Select(p => new GetAllFoodsViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                FoodImage = p.FoodImage,
                Price = p.Price,
                CommentCount = p.CommentCount,
                IsEnabled = p.IsEnabled,
                IsShowned = p.IsShowned,
                MenuName = p.Menu.Name,
                RateCount = p.RateCount,
                RatePoint = p.RatePoint,
                FoodTypeName = p.FoodType.Name,
                PlaceName = p.Menu.Place.Name
            }).ToListAsync();
            return new PagedResponse<IEnumerable<GetAllFoodsViewModel>>(result, parameter.PageNumber, parameter.PageSize);
        }
        public async Task<PagedResponse<IEnumerable<GetAllFoodsViewModel>>> GetFoodByParameter(GetFoodByParameterParameter parameter)
        {
            IQueryable<Food> foods = _foods.Include(p => p.Menu).Include(p => p.FoodType).Include(p=>p.Menu.Place).AsQueryable();
            if (parameter.FoodTypeId!= 0)
            {
                foods = foods.Where(f => f.FoodTypeId == parameter.FoodTypeId);
            }
            if (parameter.PlaceId != 0)
            {
                foods = foods.Where(f => f.Menu.PlaceId == parameter.PlaceId);
            }
            if (parameter.MenuId != 0)
            {
                foods = foods.Where(f => f.MenuId == parameter.MenuId);
            }
            var totalRecords = await foods.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Food", totalRecords);
            }
            foods = foods.Skip((parameter.PageNumber - 1) * parameter.PageSize).Take(parameter.PageSize);

            var result = await foods.Select(p => new GetAllFoodsViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                FoodImage = p.FoodImage,
                Price = p.Price,
                CommentCount = p.CommentCount,
                IsEnabled = p.IsEnabled,
                IsShowned = p.IsShowned,
                MenuName = p.Menu.Name,
                RateCount = p.RateCount,
                RatePoint = p.RatePoint,
                FoodTypeName = p.FoodType.Name,
                PlaceName=p.Menu.Place.Name,
            }).ToListAsync();
            return new PagedResponse<IEnumerable<GetAllFoodsViewModel>>(result, parameter.PageNumber, parameter.PageSize);
        }
    }
}
