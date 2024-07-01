using CleanArchitecture.Core.Features.FoodRates.Commands.CreateFoodRate;
using CleanArchitecture.Core.Features.FoodRates.Queries.GetAllFoodRates;
using CleanArchitecture.Core.Features.PlaceRates.Commands.CreatePlaceRate;
using CleanArchitecture.Core.Features.PlaceRates.Queries.GetAllPlaceRates;
using CleanArchitecture.Core.Features.PlaceRates.Queries.GetPlaceRateById;
using CleanArchitecture.Core.Features.PlaceRates.Queries.GetPlaceRatesByUserId;
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
    public class FoodRateController:BaseApiController
    {
        [HttpPost("CreateFoodRate")]
        [Authorize]
        public async Task<IActionResult> Post(CreateFoodRateParameter parameter)
        {
            CreateFoodRateCommand command = new CreateFoodRateCommand
            {
                PriceRate = parameter.PriceRate,
                FoodId = parameter.FoodId,
                TasteRate = parameter.TasteRate,
                UserId = User.Claims.FirstOrDefault(c => c.Type == "uid").Value
            };
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GetAllFoodRates")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>> Get([FromQuery] GetAllFoodRatesParameter filter)
        {
            return await Mediator.Send(new GetAllFoodRatesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber });
        }
        [HttpGet("GetFoodRateById")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetFoodRateByIdQuery { Id = id }));
        }
        [HttpGet("GetFoodRatesByUserId")]
        [Authorize]
        public async Task<PagedResponse<IEnumerable<GetAllFoodRatesViewModel>>> GetByUserId([FromQuery] GetAllFoodRatesParameter filter)
        {
            return await Mediator.Send(new GetFoodRatesByUserIdQuery()
            {
                UserId = User.Claims.FirstOrDefault(c => c.Type == "uid").Value,
                PageSize = filter.PageSize,
                PageNumber = filter.PageNumber
            });
        }
    }
}
