using CleanArchitecture.Core.Features.FoodTypes.Commands.CreateFoodType;
using CleanArchitecture.Core.Features.FoodTypes.Commands.DeleteFoodTypeById;
using CleanArchitecture.Core.Features.FoodTypes.Commands.UpdateFoodType;
using CleanArchitecture.Core.Features.FoodTypes.Queries.GetAllFoodTypes;
using CleanArchitecture.Core.Features.FoodTypes.Queries.GetFoodTypeById;
using CleanArchitecture.Core.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class FoodTypeController:BaseApiController
    {
        [HttpGet("GetAllFoodTypes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllFoodTypesViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllFoodTypesViewModel>>> Get([FromQuery] GetAllFoodTypesParameter filter)
        {
            return await Mediator.Send(new GetAllFoodTypesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber });
        }

        // GET api/<controller>/5
        [HttpGet("GetFoodTypeById")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetFoodTypeByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost("CreateFoodType")]
        public async Task<IActionResult> Post(CreateFoodTypeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("UpdateFoodType")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateFoodTypeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("DeleteFoodTypeById")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteFoodTypeByIdCommand { Id = id }));
        }

    }
}
