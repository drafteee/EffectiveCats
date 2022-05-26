using MediatR.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR.Models;

namespace EffectiveCats.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult<Task<AuthenticateResponse>> Authenticate([FromForm] Authenticate.Command command)
        {
            return _mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public ActionResult<Task<AuthenticateResponse>> RefreshToken(MediatR.Account.RefreshToken.Command command)
        {
            return _mediator.Send(command);
        }

        [HttpPost("revoke-token")]
        public ActionResult<Task<bool>> RevokeToken([FromForm] RevokeToken.Command command)
        {
            return _mediator.Send(command);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public ActionResult<Task<bool>> Register([FromBody] Register.Command command)
        {
            return _mediator.Send(command);
        }
    }
}