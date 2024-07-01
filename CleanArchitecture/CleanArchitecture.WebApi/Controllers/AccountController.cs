using CleanArchitecture.Core.DTOs.Account;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Features.Places.Commands.CreatePlace;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IPlaceService _placeService;
        public AccountController(IAccountService accountService, IPlaceService placeService)
        {
            _accountService = accountService;
            _placeService = placeService;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request, GenerateIPAddress()));
        }
        [HttpPost("authenticate-placeadmin")]
        public async Task<IActionResult> AuthenticatePlaceAdminAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticatePlaceAdminAsync(request, GenerateIPAddress()));
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = "https://restaurant-rating-fronte-84130.web.app/#";
            return Ok(await _accountService.RegisterAsync(request, origin));
        }
        [HttpPost("register-placeadmin")]
        public async Task<IActionResult> RegisterPlaceAdminAsync(RegisterPlaceAdminRequest request)
        {
            var origin = "https://restaurant-rating-fronte-84130.web.app/#";
            var placeAdmin = await _accountService.RegisterPlaceAdminAsync(request, origin);
            return Ok(placeAdmin);
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            return Ok(await _accountService.ChangePassword(request));
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery]string userId,string code)
        {
            return Ok(await _accountService.ConfirmEmailAsync(userId, code));
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            await _accountService.ForgotPassword(model);
            return Ok();
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {

            return Ok(await _accountService.ResetPassword(model));
        }
        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        [HttpDelete("delete-account")]
        public async Task<IActionResult> DeleteAccount(string userId)
        {
            return Ok(await _accountService.DeleteAccount(userId));
        }
        
    }
}