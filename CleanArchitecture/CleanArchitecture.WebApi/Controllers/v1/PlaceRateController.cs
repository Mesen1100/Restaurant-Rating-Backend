using CleanArchitecture.Core.Features.MenuTypes.Commands.CreateMenuType;
using CleanArchitecture.Core.Features.PlaceRates.Commands.CreatePlaceRate;
using CleanArchitecture.Core.Features.PlaceRates.Queries.GetAllPlaceRates;
using CleanArchitecture.Core.Features.PlaceRates.Queries.GetPlaceRateById;
using CleanArchitecture.Core.Features.PlaceRates.Queries.GetPlaceRatesByUserId;
using CleanArchitecture.Core.Features.Places.Queries.GetPlaceById;
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
    public class PlaceRateController:BaseApiController
    {
        [HttpPost("CreatePlaceRate")]
        [Authorize]
        public async Task<IActionResult> Post(CreatePlaceRateParameter parameter)
        {
            CreatePlaceRateCommand command = new CreatePlaceRateCommand
            {
                HygieneRate = parameter.HygieneRate,
                PlaceId = parameter.PlaceId,
                ServiceRate = parameter.ServiceRate,
                UserId = User.Claims.FirstOrDefault(c => c.Type == "uid").Value
            };
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GetAllPlaceRates")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>> Get([FromQuery] GetAllPlaceRatesParameter filter)
        {
            return await Mediator.Send(new GetAllPlaceRatesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber });
        }
        [HttpGet("GetPlaceRateById")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPlaceRateByIdQuery { Id = id }));
        }
        [HttpGet("GetPlaceRatesByUserId")]
        [Authorize]
        public async Task<PagedResponse<IEnumerable<GetAllPlaceRatesViewModel>>> GetByUserId([FromQuery] GetAllPlaceRatesParameter filter)
        {
            return await Mediator.Send(new GetPlaceRatesByUserIdQuery() {
                UserId = User.Claims.FirstOrDefault(c => c.Type == "uid").Value,
                PageSize = filter.PageSize, 
                PageNumber = filter.PageNumber });
        }
    }
}
