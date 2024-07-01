using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Repository;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Core.Features.Places.Queries.GetAllPlaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Features.Places.Queries.GetTopNPlaceByRatePoint;
using CleanArchitecture.Core.Features.Places.Queries.GetPlacesByName;
using CleanArchitecture.Core.Features.Menus.Queries.GetAllMenus;
using CleanArchitecture.Core.Features.Places.Queries.GetPlacesByCityId;
using CleanArchitecture.Core.Features.Places.Queries.GetPlaceByDistrictId;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class PlaceRepositoryAsync : GenericRepositoryAsync<Place>, IPlaceRepositoryAsync
    {
        private readonly DbSet<Place> _place;
        private readonly DbContext _dbContext;
        public PlaceRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _place = dbContext.Set<Place>();
            _dbContext = dbContext;
        }

        public async Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> GetAllPlaces(GetAllPlacesParameter parameter){
            IQueryable<Place> places = _place.Include(p => p.User).Include(p => p.PlaceType).Include(p=>p.City).Include(p=>p.District).AsQueryable();
            var totalRecords = await places.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Place",totalRecords);
            }
            places = places.Skip((parameter.PageNumber - 1) * parameter.PageSize).Take(parameter.PageSize);
            var result = await places.Select(p => new GetAllPlacesViewModel
            {
                Id=p.Id,
                Name=p.Name,
                Description=p.Description,
                Address=p.Address,
                CityName=p.City.Name,
                RateCount=p.RateCount,
                RatePoint=p.RatePoint,
                DistrictName=p.District.Name,
                PlaceTypeName=p.PlaceType.Name,
                ManagerName=p.User.Username,
            }).ToListAsync();
            return new PagedResponse<IEnumerable<GetAllPlacesViewModel>>(result, parameter.PageNumber, parameter.PageSize);

        }
        public async Task<Place> GetPlaceAsync(int id)
        {
            return await _place.Where(p => p.Id == id)
                                    .FirstOrDefaultAsync();
        }
        public async Task<Response<GetAllPlacesViewModel>> GetPlaceById(int id)
        {
            var place = await _place.Include(p => p.User).Include(p => p.PlaceType).Include(p=>p.City).Include(p=>p.District).FirstOrDefaultAsync(p => p.Id == id);
            if (place == null)
            {
                throw new EntityNotFoundException("Place", 0);
            }
            var result = new GetAllPlacesViewModel
            {
                Id = place.Id,
                Name = place.Name,
                Description = place.Description,
                Address = place.Address,
                CityName = place.City.Name,
                RatePoint=place.RatePoint,
                RateCount=place.RateCount,
                DistrictName = place.District.Name,
                PlaceTypeName = place.PlaceType.Name,
                ManagerName = place.User.Username
            };
            return new Response<GetAllPlacesViewModel>(result);
        }
        public Place RateUpdateAsync(PlaceRate placeRate)
        {
            var place = _place.FirstOrDefault(p => p.Id == placeRate.PlaceId);
            if (place == null) { throw new EntityNotFoundException("Place", place.Id); }
            double oldRatePoint = place.RatePoint * place.RateCount;
            double newRateCount = place.RateCount + 1;
            double newRatePoint = (oldRatePoint + placeRate.RatePoint) / (newRateCount);
            place.RatePoint = newRatePoint;
            place.RateCount = newRateCount;
            return place;
        }
        public async Task<Response<IEnumerable<GetTopNPlaceByRatePointViewModel>>> GetTopNPlaceByRatePoint(int n,bool direction)
        {
            //direction for increase rate point or decrease rate point
            //true is decrease 
            //false is increase
            IQueryable<Place> places;

            if (direction)
            {
                // Decreasing order
                places = _place
                    .Include(p => p.User)
                    .Include(p => p.PlaceType)
                    .Include(p=>p.City)
                    .Include(p=>p.District)
                    .OrderByDescending(p => p.RatePoint)
                    .Take(n)
                    .AsQueryable();
            }
            else
            {
                // Increasing order
                places = _place
                    .Include(p => p.User)
                    .Include(p => p.PlaceType)
                    .Include(p => p.City)
                    .Include(p => p.District)
                    .OrderBy(p => p.RatePoint)
                    .Take(n)
                    .AsQueryable();
            }
            var totalRecords = await places.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Place", totalRecords);
            }
            var result = await places.Select(p => new GetTopNPlaceByRatePointViewModel
            {
                Id = p.Id,
                Name = p.Name,
                City = p.City.Name,
                RatePoint=p.RatePoint,
                District = p.City.Name,
                PlaceTypeName = p.PlaceType.Name,
            }).ToListAsync();
            return new Response<IEnumerable<GetTopNPlaceByRatePointViewModel>>(result);

        }
        public async Task<Response<int>> CreatePlaceAsync(Place place)
        {
            await _dbContext.AddAsync(place);
            await _dbContext.SaveChangesAsync();
            return new Response<int>(place.Id);
           
        }
        public async Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> GetPlacesByName(GetPlacesByNameParameter parameter)
        {
            IQueryable<Place> places= _place.Include(p => p.User).Include(p => p.PlaceType).Include(p => p.City).Include(p => p.District).AsQueryable();
            if (!string.IsNullOrEmpty(parameter.SearchString))
            {
                places = places.Where(f => f.Name.Contains(parameter.SearchString));
            }
            if (parameter.CityId != 0)
            {
                places = places.Where(f => f.CityId == parameter.CityId);
            }
            if (parameter.PlaceTypeId != 0) 
            {
                places = places.Where(f => f.PlaceTypeId == parameter.PlaceTypeId);
            }
            var totalRecords = await places.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Place", totalRecords);
            }
            places = places.Skip((parameter.PageNumber - 1) * parameter.PageSize).Take(parameter.PageSize);

            var result = await places.Select(p => new GetAllPlacesViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Address = p.Address,
                RateCount=p.RateCount,
                RatePoint=p.RatePoint,
                CityName = p.City.Name,
                DistrictName = p.District.Name,
                PlaceTypeName = p.PlaceType.Name,
                ManagerName = p.User.Username,
            }).ToListAsync();
            return new PagedResponse<IEnumerable<GetAllPlacesViewModel>>(result, parameter.PageNumber, parameter.PageSize);
        }
        public async Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> GetPlaceByCityId(GetPlaceByCityIdParameter parameter)
        {
            IQueryable<Place> places = _place.Include(p => p.User).Include(p => p.PlaceType).Include(p => p.City).Include(p => p.District).AsQueryable();
            if (parameter.CityId!=0)
            {
                places = places.Where(f => f.CityId==parameter.CityId);
            }
            if (parameter.PlaceTypeId != 0) 
            {
                places = places.Where(f => f.PlaceTypeId == parameter.PlaceTypeId);
            }
            var totalRecords = await places.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Place", totalRecords);
            }
            places = places.Skip((parameter.PageNumber - 1) * parameter.PageSize).Take(parameter.PageSize);

            var result = await places.Select(p => new GetAllPlacesViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Address = p.Address,
                CityName = p.City.Name,
                RateCount=p.RateCount,
                RatePoint=p.RatePoint,
                DistrictName = p.District.Name,
                PlaceTypeName = p.PlaceType.Name,
                ManagerName = p.User.Username,
            }).ToListAsync();
            return new PagedResponse<IEnumerable<GetAllPlacesViewModel>>(result, parameter.PageNumber, parameter.PageSize);
        }
        public async Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> GetPlaceByDistrictId(GetPlaceByDistrictIdParameter parameter)
        {
            IQueryable<Place> places = _place.Include(p => p.User).Include(p => p.PlaceType).Include(p => p.City).Include(p => p.District).AsQueryable();
            if (parameter.DistrictId != 0)
            {
                places = places.Where(f => f.DistrictId == parameter.DistrictId);
            }
            var totalRecords = await places.CountAsync();
            if (totalRecords == 0)
            {
                throw new EntityNotFoundException("Place", totalRecords);
            }
            places = places.Skip((parameter.PageNumber - 1) * parameter.PageSize).Take(parameter.PageSize);

            var result = await places.Select(p => new GetAllPlacesViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Address = p.Address,
                CityName = p.City.Name,
                RateCount = p.RateCount,
                RatePoint = p.RatePoint,
                DistrictName = p.District.Name,
                PlaceTypeName = p.PlaceType.Name,
                ManagerName = p.User.Username,
            }).ToListAsync();
            return new PagedResponse<IEnumerable<GetAllPlacesViewModel>>(result, parameter.PageNumber, parameter.PageSize);
        }
        public Place GetPlaceIdByUserId(string userId)
        {
            var placeTask = _place.Where(p => p.ManagerUserId == userId)
                                  .FirstOrDefaultAsync();
            var place = placeTask.GetAwaiter().GetResult();

            if (place == null)
            {
                throw new Exception("Place not found.");
            }

            return place;
        }
    }
}
