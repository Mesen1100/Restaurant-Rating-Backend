using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
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
    public class PlaceTypeRepositoryAsync : GenericRepositoryAsync<PlaceType>, IPlaceTypeRepositoryAsync
    {
        private readonly DbSet<PlaceType> _placetypes;
        public PlaceTypeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _placetypes = dbContext.Set<PlaceType>();
        }
    }
}
