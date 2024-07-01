
using CleanArchitecture.Core.Features.Users.Queries.GetAllUsers;
using CleanArchitecture.Core.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    public class UserController:BaseApiController
    {
        // GET: api/<controller>
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<GetAllUsersViewModel>>))]
        public async Task<PagedResponse<IEnumerable<GetAllUsersViewModel>>> Get([FromQuery] GetAllUsersParameter filter)
        {
            return await Mediator.Send(new GetAllUsersQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber });
        }
    }
}
