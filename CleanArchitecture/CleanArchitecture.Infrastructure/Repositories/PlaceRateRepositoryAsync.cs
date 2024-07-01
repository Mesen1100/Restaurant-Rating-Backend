using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Features.PlaceRates.Queries.GetAllPlaceRates;
using CleanArchitecture.Core.Features.Places.Queries.GetAllPlaces;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class PlaceRateRepositoryAsync : GenericRepositoryAsync<PlaceRate>, IPlaceRateRepositoryAsync
    {
        private readonly DbSet<PlaceRate> _placeRates;
        public PlaceRateRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _placeRates=dbContext.Set<PlaceRate>();
        }

        public async Task<PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>> GetAllPlaceRates(GetAllPlaceRatesParameter parameter)
        {
            IQueryable<PlaceRate> placeRates = _placeRates.Include(p => p.User).Include(p => p.Place).AsQueryable();
            var totalRecords = await placeRates.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Place Rates", totalRecords);
            }
            placeRates = placeRates.Skip((parameter.PageNumber - 1) * parameter.PageSize).Take(parameter.PageSize);
            var result = await placeRates.Select(p => new GetAllPlaceRatesViewModel
            {
                Id=p.Id,
                HygieneRate=p.HygieneRate,
                ServiceRate=p.ServiceRate,
                PlaceName=p.Place.Name,
                UserName=p.User.Username
            }).ToListAsync();
            return new PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>(result, parameter.PageNumber, parameter.PageSize);

        }
        public async Task<Response<GetAllPlaceRatesViewModel>> GetPlaceRateById(int id)
        {
            var placeRate = await _placeRates.Include(p => p.User).Include(p => p.Place).FirstOrDefaultAsync(p => p.Id == id);
            if (placeRate == null)
            {
                throw new EntityNotFoundException("Place Rate", id);
            }
            var result = new GetAllPlaceRatesViewModel
            {
                Id = placeRate.Id,
                HygieneRate=placeRate.HygieneRate,
                ServiceRate=placeRate.ServiceRate,
                PlaceName=placeRate.Place.Name,
                UserName=placeRate.User.Username
            };
            return new Response<GetAllPlaceRatesViewModel>(result);
        }
        public async Task<PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>> GetPlaceByUserId(string userId,GetAllPlaceRatesParameter parameter)
        {
            IQueryable<PlaceRate> placeRates = _placeRates
                                                           .Include(p => p.User)
                                                           .Include(p => p.Place)
                                                           .Where(p=>p.UserId==userId)
                                                           .AsQueryable();
            var totalRecords = await placeRates.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Place Rates", totalRecords);
            }
            placeRates = placeRates.Skip((parameter.PageNumber - 1) * parameter.PageSize).Take(parameter.PageSize);
            var result = await placeRates.Select(p => new GetAllPlaceRatesViewModel
            {
                Id = p.Id,
                HygieneRate = p.HygieneRate,
                ServiceRate = p.ServiceRate,
                PlaceName = p.Place.Name,
                UserName = p.User.Username
            }).ToListAsync();
            return new PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>(result, parameter.PageNumber, parameter.PageSize);
        }
        
    }
}
