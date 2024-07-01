using CleanArchitecture.Core.Features.Cities.Commands.CreateCity;
using CleanArchitecture.Core.Features.Cities.Queries.GetAllCities;
using CleanArchitecture.Core.Features.Cities.Queries.GetCityById;
using CleanArchitecture.Core.Features.Districts.Commands;
using CleanArchitecture.Core.Features.Districts.Queries.GetDistrictByCityId;
using CleanArchitecture.Core.Features.Places.Queries.GetAllPlaces;
using CleanArchitecture.Core.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class LocationController:BaseApiController
    {
        [HttpGet("GetAllCities")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllCitiesViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllCitiesViewModel>>> GetCity()
        {
            return await Mediator.Send(new GetAllCitiesQuery());
        }

        [HttpGet("GetCityById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<GetAllCitiesViewModel>))]
        public async Task<Response<GetAllCitiesViewModel>> GetCityById(int id)
        {
            return await Mediator.Send(new GetCityByIdQuery { Id = id });
        }
        [HttpPost("CreateCity")]
        public async Task<Response<int>> CreateCity(CreateCityCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpGet("GetDistrictByCityId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetDistrictByCityIdViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetDistrictByCityIdViewModel>>> GetDistrict([FromQuery] int cityId)
        {
            return await Mediator.Send(new GetDistrictByCityIdQuery() { CityId = cityId }); ;
        }
        [HttpPost("CreateDistrict")]
        public async Task<Response<int>> CreateDistrict(CreateDistrictCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpGet("GetTime")]
        public string GetTime()
        {
            DateTime date = DateTime.UtcNow;
            DateTime utc3 = date.AddHours(3);
            return utc3.ToString("HH : mm");
        }
    }
}
