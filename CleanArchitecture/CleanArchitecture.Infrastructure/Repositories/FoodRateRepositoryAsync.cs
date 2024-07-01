using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Features.FoodRates.Queries.GetAllFoodRates;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class FoodRateRepositoryAsync : GenericRepositoryAsync<FoodRate>, IFoodRateRepositoryAsync
    {
        private readonly DbSet<FoodRate> _foodRates;
        public FoodRateRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _foodRates = dbContext.Set<FoodRate>();
        }

        public async Task<PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>> GetAllFoodRates(GetAllFoodRatesParameter parameter)
        {
            IQueryable<FoodRate> foodRates = _foodRates.Include(p => p.User).Include(p => p.Food).AsQueryable();
            var totalRecords = await foodRates.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Food Rates", totalRecords);
            }
            foodRates = foodRates.Skip((parameter.PageNumber - 1) * parameter.PageSize).Take(parameter.PageSize);
            var result = await foodRates.Select(p => new GetAllFoodRatesViewModel
            {
                Id = p.Id,
                TasteRate=p.TasteRate,
                PriceRate=p.PriceRate,
                FoodName=p.Food.Name,
                UserName = p.User.Username
            }).ToListAsync();
            return new PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>(result, parameter.PageNumber, parameter.PageSize);

        }
        public async Task<Response<GetAllFoodRatesViewModel>> GetFoodRateById(int id)
        {
            var foodRate = await _foodRates.Include(p => p.User).Include(p => p.Food).FirstOrDefaultAsync(p => p.Id == id);
            if (foodRate == null)
            {
                throw new EntityNotFoundException("Food Rate", id);
            }
            var result = new GetAllFoodRatesViewModel
            {
                Id = foodRate.Id,
                TasteRate=foodRate.TasteRate,
                PriceRate=foodRate.PriceRate,
                FoodName=foodRate.Food.Name,
                UserName = foodRate.User.Username
            };
            return new Response<GetAllFoodRatesViewModel>(result);
        }
        public async Task<PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>> GetFoodRateByUserId(string userId, GetAllFoodRatesParameter parameter)
        {
            IQueryable<FoodRate> foodRates = _foodRates
                                                           .Include(p => p.User)
                                                           .Include(p => p.Food)
                                                           .Where(p => p.UserId == userId)
                                                           .AsQueryable();
            var totalRecords = await foodRates.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Food Rates", totalRecords);
            }
            foodRates = foodRates.Skip((parameter.PageNumber - 1) * parameter.PageSize).Take(parameter.PageSize);
            var result = await foodRates.Select(p => new GetAllFoodRatesViewModel
            {
                Id = p.Id,
                FoodName=p.Food.Name,
                PriceRate=p.PriceRate,
                TasteRate=p.TasteRate,
                UserName = p.User.Username
            }).ToListAsync();
            return new PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>(result, parameter.PageNumber, parameter.PageSize);
        }
    }
}
