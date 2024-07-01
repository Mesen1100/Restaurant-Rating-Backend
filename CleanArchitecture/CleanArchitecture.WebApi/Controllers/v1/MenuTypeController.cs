using CleanArchitecture.Core.Features.MenuTypes.Commands.CreateMenuType;
using CleanArchitecture.Core.Features.MenuTypes.Commands.DeleteMenuTypeById;
using CleanArchitecture.Core.Features.MenuTypes.Commands.UpdateMenuType;
using CleanArchitecture.Core.Features.MenuTypes.Queries.GetAllMenuTypes;
using CleanArchitecture.Core.Features.MenuTypes.Queries.GetMenuTypeById;
using CleanArchitecture.Core.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MenuTypeController:BaseApiController
    {
        [HttpGet("GetAllMenuTypes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllMenuTypesViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllMenuTypesViewModel>>> Get([FromQuery] GetAllMenuTypesParameter filter)
        {
            return await Mediator.Send(new GetAllMenuTypesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber });
        }

        // GET api/<controller>/5
        [HttpGet("GetMenuTypeById")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetMenuTypeByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost("CreateMenuType")]
        public async Task<IActionResult> Post(CreateMenuTypeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("UpdateMenuType")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateMenuTypeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("DeleteMenuTypeById")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteMenuTypeByIdCommand { Id = id }));
        }

    }
}
