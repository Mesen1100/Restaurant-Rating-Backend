using CleanArchitecture.Core.Features.Places.Commands.CreatePlace;
using CleanArchitecture.Core.Features.Places.Commands.DeletePlaceById;
using CleanArchitecture.Core.Features.Places.Commands.UpdatePlace;
using CleanArchitecture.Core.Features.Places.Queries.GetAllPlaces;
using CleanArchitecture.Core.Features.Places.Queries.GetPlaceByDistrictId;
using CleanArchitecture.Core.Features.Places.Queries.GetPlaceById;
using CleanArchitecture.Core.Features.Places.Queries.GetPlacesByCityId;
using CleanArchitecture.Core.Features.Places.Queries.GetPlacesByDistrictId;
using CleanArchitecture.Core.Features.Places.Queries.GetPlacesByName;
using CleanArchitecture.Core.Features.Places.Queries.GetTopNPlaceByRatePoint;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PlaceController:BaseApiController
    {
        [HttpGet("GetAllPlaces")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllPlacesViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> Get([FromQuery] GetAllPlacesParameter filter)
        {
            return await Mediator.Send(new GetAllPlacesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber });
        }

        // GET api/<controller>/5
        [HttpGet("GetPlaceById")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPlaceByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost("CreatePlace")]
        [Authorize]
        public async Task<IActionResult> Post(CreatePlaceRequestParameter request)
        {
            CreatePlaceCommand command = new CreatePlaceCommand
            {
                Name = request.Name,
                Description = request.Description,
                Address = request.Address,
                CityId= request.CityId,
                DistrictId = request.DistrictId,
                PlaceTypeId = request.PlaceTypeId,
                ManagerUserId = User.Claims.FirstOrDefault(c => c.Type == "uid").Value
            };
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("UpdatePlace")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdatePlaceRequestParameter request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }
            UpdatePlaceCommand command = new UpdatePlaceCommand
            {
                Name = request.Name,
                Description = request.Description,
            };
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("DeletePlaceById")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            DeletePlaceByIdCommand command = new DeletePlaceByIdCommand
            {
                Id = id,
                ManagerUserId = User.Claims.FirstOrDefault(c => c.Type == "uid").Value
            };
            return Ok(await Mediator.Send(command));
        }
        [HttpGet("GetTopNPlacesByRatePoint")]
        public async Task<Response<IEnumerable<GetTopNPlaceByRatePointViewModel>>> Get([FromQuery]GetTopNPlaceByRatePointQuery filter)
        {
            return await Mediator.Send(filter);
        }
        [HttpGet("GetPlaceByName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllPlacesViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> GetPlaceByName([FromQuery] GetPlacesByNameParameter filter)
        {
            return await Mediator.Send(new GetPlacesByNameQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber,SearchString=filter.SearchString,CityId=filter.CityId,PlaceTypeId=filter.PlaceTypeId });
        }
        [HttpGet("GetPlaceByCityId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllPlacesViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> GetPlaceByCityId([FromQuery] GetPlaceByCityIdParameter filter)
        {
            return await Mediator.Send(new GetPlaceByCityIdQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber,CityId=filter.CityId,PlaceTypeId=filter.PlaceTypeId });
        }
        [HttpGet("GetPlaceByDistrictId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllPlacesViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllPlacesViewModel>>> GetPlaceByDistrictId([FromQuery] GetPlaceByDistrictIdParameter filter)
        {
            return await Mediator.Send(new GetPlaceByDistrictIdQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber,DistrictId=filter.DistrictId});
        }
    }
}
