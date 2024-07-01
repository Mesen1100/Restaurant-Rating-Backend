using CleanArchitecture.Core.Features.PlaceTypes.Commands.CreatePlaceType;
using CleanArchitecture.Core.Features.PlaceTypes.Commands.DeletePlaceTypeById;
using CleanArchitecture.Core.Features.PlaceTypes.Commands.UpdatePlaceType;
using CleanArchitecture.Core.Features.PlaceTypes.Queries.GetAllPlaceTypes;
using CleanArchitecture.Core.Features.PlaceTypes.Queries.GetPlaceTypeById;
using CleanArchitecture.Core.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PlaceTypeController:BaseApiController
    {
        [HttpGet("GetAllPlaceTypes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllPlaceTypesViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllPlaceTypesViewModel>>> Get([FromQuery] GetAllPlaceTypesParameter filter)
        {
            return await Mediator.Send(new GetAllPlaceTypesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber });
        }

        // GET api/<controller>/5
        [HttpGet("GetPlaceTypeById")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPlaceTypeByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost("CreatePlaceType")]
        public async Task<IActionResult> Post(CreatePlaceTypeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("UpdatePlaceType")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdatePlaceTypeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("DeletePlaceTypeById")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeletePlaceTypeByIdCommand { Id = id }));
        }

    }
}
