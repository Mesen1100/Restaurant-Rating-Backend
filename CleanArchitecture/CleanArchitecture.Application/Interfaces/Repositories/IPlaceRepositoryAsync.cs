using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Features.Places.Queries.GetAllPlaces;
using CleanArchitecture.Core.Features.Places.Queries.GetPlaceByDistrictId;
using CleanArchitecture.Core.Features.Places.Queries.GetPlacesByCityId;
using CleanArchitecture.Core.Features.Places.Queries.GetPlacesByName;
using CleanArchitecture.Core.Features.Places.Queries.GetTopNPlaceByRatePoint;
using CleanArchitecture.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IPlaceRepositoryAsync : IGenericRepositoryAsync<Place>
    {
        Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> GetAllPlaces(GetAllPlacesParameter parameter);
        Task<Response<GetAllPlacesViewModel>> GetPlaceById(int id);
        Task<Place> GetPlaceAsync(int id);
        Place RateUpdateAsync(PlaceRate placeRate);
        Task<Response<IEnumerable<GetTopNPlaceByRatePointViewModel>>> GetTopNPlaceByRatePoint(int n, bool direction);
        Task<Response<int>> CreatePlaceAsync(Place Place);
        Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> GetPlacesByName(GetPlacesByNameParameter parameter);
        Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> GetPlaceByDistrictId(GetPlaceByDistrictIdParameter parameter);
        Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> GetPlaceByCityId(GetPlaceByCityIdParameter parameter);
        Place GetPlaceIdByUserId(string userId);
    }
}
