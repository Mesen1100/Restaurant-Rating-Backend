using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IUserRepositoryAsync : IGenericRepositoryAsync<User>
    {
        User GetById(string id);
        Task<Response<string>> DeleteAccount(string userId);
        string GetUserIdByEmail(string email);
    }
}
