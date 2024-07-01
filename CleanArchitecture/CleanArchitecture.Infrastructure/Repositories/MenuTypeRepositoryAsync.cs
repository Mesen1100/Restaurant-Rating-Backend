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
    public class MenuTypeRepositoryAsync : GenericRepositoryAsync<MenuType>, IMenuTypeRepositoryAsync
    {
        private readonly DbSet<MenuType> _menuTypes;
        public MenuTypeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _menuTypes= dbContext.Set<MenuType>();
        }
    }
}
