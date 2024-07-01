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
    public class FoodTypeRepositoryAsync:GenericRepositoryAsync<FoodType>, IFoodTypeRepositoryAsync
    {
        private readonly DbSet<FoodType> _foodTypes;
        public FoodTypeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _foodTypes = dbContext.Set<FoodType>();
        }
    }
}
