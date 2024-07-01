using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Seeds
{
    public static class DefaultPlaceAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, IUserRepositoryAsync userRepository, RoleManager<IdentityRole> roleManager)
        {
            //Seed Place Admin
            var placeAdmin = new ApplicationUser
            {
                UserName = "placeadmin",
                Email = "placeadmin@gmail.com",
                FirstName = "Max",
                LastName = "Smith",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != placeAdmin.Id))
            {
                var user =await userManager.FindByEmailAsync(placeAdmin.Email);
                if(user==null)
                {
                    var newUser = new User
                    {
                        Id = placeAdmin.Id,
                        FirstName = placeAdmin.FirstName,
                        LastName = placeAdmin.LastName,
                        Username = placeAdmin.UserName,
                        Email = placeAdmin.Email,
                        Role = Roles.PlaceAdmin.ToString(),
                    };
                    await userManager.CreateAsync(placeAdmin, "Demo1234.");
                    await userRepository.AddAsync(newUser);
                    await userManager.AddToRoleAsync(placeAdmin, Roles.PlaceAdmin.ToString());
                }
            }
        }
    }
}
