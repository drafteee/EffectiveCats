using Common.Account;
using Domain.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EffectiveCats.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        public UserController()
        {

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate([FromForm] Authenticate.Command command)
        {
            var response = await Mediator.Send(command);
            Response.Headers.Add("Authorization", "Bearer " + response.JwtToken);
            return response;
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthenticateResponse>> RefreshToken(Common.Account.RefreshToken.Command command)
        {
            var response = await Mediator.Send(command);
            Response.Headers.Add("Authorization", "Bearer " + response.JwtToken);
            return response;
        }

        [HttpPost("revoke-token")]
        public async Task<ActionResult<bool>> RevokeToken([FromForm] RevokeToken.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ActionResult<bool>> Register([FromBody] Register.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}