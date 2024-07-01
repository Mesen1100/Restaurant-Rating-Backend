using CleanArchitecture.Core.Features.Foods.Commands.CreateFood;
using CleanArchitecture.Core.Features.Foods.Commands.DeleteFoodById;
using CleanArchitecture.Core.Features.Foods.Commands.UpdateFood;
using CleanArchitecture.Core.Features.Foods.Queries.GetAllFoods;
using CleanArchitecture.Core.Features.Foods.Queries.GetFoodById;
using CleanArchitecture.Core.Features.Foods.Queries.GetFoodsByMenuId;
using CleanArchitecture.Core.Features.Foods.Queries.GetFoodsByName;
using CleanArchitecture.Core.Features.Foods.Queries.GetTopNFoodByRatePoint;
using CleanArchitecture.Core.Features.Menus.Commands.UpdateMenu;
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
    public class FoodController:BaseApiController
    {
        [HttpGet("GetAllFoods")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllFoodsViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllFoodsViewModel>>> Get([FromQuery] GetAllFoodsParameter filter)
        {
            return await Mediator.Send(new GetAllFoodsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber });
        }
        [HttpGet("GetFoodById")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetFoodByIdQuery { Id = id }));
        }
        [HttpGet("GetFoodByParameter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllFoodsViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllFoodsViewModel>>> GetFoodByParameter([FromQuery] GetFoodByParameterParameter filter)
        {
            return await Mediator.Send(new GetFoodByParameterQuery() { 
                PageSize = filter.PageSize, 
                PageNumber = filter.PageNumber,
                FoodTypeId =filter.FoodTypeId,
                PlaceId=filter.PlaceId,
                MenuId=filter.MenuId,
            });
        }

        [HttpPost("CreateFood")]
        [Authorize]
        public async Task<IActionResult> Post(CreateFoodParameter parameter)
        {
            CreateFoodCommand command = new CreateFoodCommand
            {
                Name = parameter.Name,
                Description = parameter.Description,
                FoodImage = parameter.FoodImage,
                Price = parameter.Price,
                FoodTypeId = parameter.FoodTypeId,
                MenuId = parameter.MenuId,
                ManagerUserId = User.Claims.FirstOrDefault(c => c.Type == "uid").Value
            };
            return Ok(await Mediator.Send(command));
        }
        [HttpPut("UpdateFood")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateFoodParameter request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }
            UpdateFoodCommand command = new UpdateFoodCommand
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                FoodImage = request.FoodImage,
                Price = request.Price,
                ManagerUserId = User.Claims.FirstOrDefault(c => c.Type == "uid").Value
            };
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("DeleteFoodById")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteFoodByIdCommand command = new DeleteFoodByIdCommand
            {
                Id = id,
                ManagerUserId = User.Claims.FirstOrDefault(c => c.Type == "uid").Value
            };
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GetTopNFoodByRatePoint")]
        public async Task<IActionResult> Get([FromQuery]GetTopNFoodByRatePointQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet("GetFoodsByName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllFoodsViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllFoodsViewModel>>> GetFoodByName([FromQuery] GetFoodsByNameParameter filter)
        {
            return await Mediator.Send(new GetFoodsByNameQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber,SearchString=filter.SearchString });
        }
    }
}
