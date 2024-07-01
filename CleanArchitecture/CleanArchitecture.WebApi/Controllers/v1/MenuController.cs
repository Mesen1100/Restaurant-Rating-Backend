using CleanArchitecture.Core.Features.Menus.Commands.CreateMenu;
using CleanArchitecture.Core.Features.Menus.Commands.DeleteMenuById;
using CleanArchitecture.Core.Features.Menus.Commands.UpdateMenu;
using CleanArchitecture.Core.Features.Menus.Queries.GetAllMenus;
using CleanArchitecture.Core.Features.Menus.Queries.GetMenuById;
using CleanArchitecture.Core.Features.Menus.Queries.GetMenuByPlaceId;
using CleanArchitecture.Core.Features.Menus.Queries.GetMenusByName;
using CleanArchitecture.Core.Features.Menus.Queries.GetTopNMenuByRatePoint;
using CleanArchitecture.Core.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MenuController: BaseApiController
    {
        [HttpGet("GetAllMenus")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllMenusViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllMenusViewModel>>> Get([FromQuery] GetAllMenusParameter filter)
        {
            return await Mediator.Send(new GetAllMenusQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber });
        }
        [HttpGet("GetMenuByPlaceId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllMenusViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllMenusViewModel>>> GetMenuByPlaceId([FromQuery] GetMenuByPlaceIdParameter filter)
        {
            return await Mediator.Send(new GetMenuByPlaceIdQuery () { PageSize = filter.PageSize, PageNumber = filter.PageNumber,PlaceId=filter.PlaceId,MenuTypeId=filter.MenuTypeId });
        }

        // GET api/<controller>/5
        [HttpGet("GetMenuById")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetMenuByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost("CreateMenu")]
        [Authorize]
        public async Task<IActionResult> Post(CreateMenuRequestParameter request)
        {
            CreateMenuCommand command = new CreateMenuCommand
            {
                Name = request.Name,
                Description=request.Description,
                PlaceId=request.PlaceId,
                MenuTypeId = request.MenuTypeId,
                ManagerUserId = User.Claims.FirstOrDefault(c => c.Type == "uid").Value
            };
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("UpdateMenu")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateMenuRequestParameter request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }
            UpdateMenuCommand command = new UpdateMenuCommand
            {
                Id=request.Id,
                Name = request.Name,
                Description = request.Description,
                MenuTypeId = request.MenuTypeId,
                ManagerUserId = User.Claims.FirstOrDefault(c => c.Type == "uid").Value
            };
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("DeleteMenuById")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteMenuByIdCommand command = new DeleteMenuByIdCommand
            {
                Id = id,
                ManagerUserId = User.Claims.FirstOrDefault(c => c.Type == "uid").Value
            };
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GetTopNMenuByRatePoint")]
        public async Task<IActionResult> Get([FromQuery]GetTopNMenuByRatePointQuery request)
        {
            return Ok(await Mediator.Send(request));
        }
        [HttpGet("GetMenuByName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllMenusViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllMenusViewModel>>> GetMenusByName([FromQuery] GetMenusByNameQuery filter)
        {
            return await Mediator.Send(new GetMenusByNameQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber,SearchString=filter.SearchString });
        }
    }
}
