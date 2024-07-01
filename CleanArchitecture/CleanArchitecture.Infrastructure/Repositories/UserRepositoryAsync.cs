using CleanArchitecture.Core.Entities;
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
    public class UserRepositoryAsync : GenericRepositoryAsync<User>, IUserRepositoryAsync
    {
        private readonly DbSet<User> _users;
        private readonly DbContext _dbContext;
        public UserRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _users = dbContext.Set<User>();
            _dbContext = dbContext;
        }

        public async Task<Response<string>> DeleteAccount(string userId)
        {
            var user = GetById(userId);
            if (user == null)
            {
                throw new InvalidOperationException();
            }
            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync();
            return new Response<string>(userId);

        }

        public User GetById(string id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }
        public string GetUserIdByEmail(string email)
        {
            User user = _users.FirstOrDefault(x => x.Email == email);
            string userId = user.Id;
            return userId;
        }
    }
}
