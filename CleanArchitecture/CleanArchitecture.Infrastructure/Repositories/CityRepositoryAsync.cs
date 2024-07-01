using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Features.Cities.Queries.GetAllCities;
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
    public class CityRepositoryAsync : GenericRepositoryAsync<City>, ICityRepositoryAsync
    {
        private readonly DbSet<City> _cities;

        public CityRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _cities = dbContext.Set<City>();
        }

        public async Task<PagedResponse<IEnumerable<GetAllCitiesViewModel>>> GetAllCities()
        {
            IQueryable<City> cities= _cities.AsQueryable();
            var totalrecords= cities.Count();
            if (totalrecords == 0)
            {
                throw new EntityNotFoundException("City",0);
            }
            var result = await cities.Select(c => new GetAllCitiesViewModel
            {
                Id=c.Id,
                Name = c.Name
            }).ToListAsync();
            return new PagedResponse<IEnumerable<GetAllCitiesViewModel>>(result,1,81);
        }
    }
}
