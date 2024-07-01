using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Features.Districts.Queries.GetDistrictByCityId;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class DistrictRepositoryAsync:GenericRepositoryAsync<District>,IDistrictRepositoryAsync
    {
        private readonly DbSet<District> _districts;

        public DistrictRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _districts = dbContext.Set<District>();
        }

        public async Task<PagedResponse<IEnumerable<GetDistrictByCityIdViewModel>>> GetDistrictByCityId(int cityId)
        {
            // Filter districts by cityId
            var dist = _districts.Include(e => e.City)
                                 .Where(e => e.CityId == cityId)
                                 .AsQueryable();

            // Get the total record count after filtering
            var totalrecords = await dist.CountAsync();

            // If no districts found, throw an exception
            if (totalrecords == 0)
            {
                throw new EntityNotFoundException("District", 0);
            }

            // Retrieve the filtered districts
            var result = await dist.Select(d => new GetDistrictByCityIdViewModel
            {
                Id=d.Id,
                Name = d.Name,
                CityName = d.City.Name,
            }).ToListAsync();

            // Create and return the paged response
            return new PagedResponse<IEnumerable<GetDistrictByCityIdViewModel>>(result, 1, 30);
        }
    }
}
