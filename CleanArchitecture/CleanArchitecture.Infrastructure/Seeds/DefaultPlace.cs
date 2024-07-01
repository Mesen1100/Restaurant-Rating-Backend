using CleanArchitecture.Core.Entities;
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
    public static class DefaultPlace
    {
        public static async Task SeedAsync(IUserRepositoryAsync userRepository,IPlaceRepositoryAsync placeRepositoryAsync, IPlaceTypeRepositoryAsync placeTypeRepositoryAsync)
        {
            var placeType = new PlaceType {
                Name = "Lokanta"
            };
            await placeTypeRepositoryAsync.AddAsync(placeType);
            var place = new Place
            {
                Name = "Default Place",
                PlaceTypeId = placeType.Id,
                Description = "This Place For Demo",
                ManagerUserId = userRepository.GetUserIdByEmail("placeadmin@gmail.com"),
                Address = "Somewhere in Türkiye",
                CityId = 1,
                DistrictId = 1,
            };
            await placeRepositoryAsync.AddAsync(place);
            
        }
    }
}
