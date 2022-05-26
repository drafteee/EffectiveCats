using MediatR.MCat;
using MediatR.Cat.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EffectiveCats.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    public class CatController : ControllerBase
    {
        private IMediator _mediator;

        public CatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public ActionResult<Task<long>> Create([FromForm] CreateCat.Command command)
        {
            return _mediator.Send(command);
        }

        /// <summary>
        ///     Logins.
        /// </summary>
        /// <param name="request">Model as a <see cref="LoginRequest" />.</param>
        /// <response code="200">Logged in successfully.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">User is not registered in the system.</response>
        /// <example>POST: .auth/login</example>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByIdCatResponse))]
        [HttpGet("getById")]
        public ActionResult<Task<GetByIdCatResponse>> GetById([FromQuery] GetByIdCat.Request request)
        {
            return _mediator.Send(request);
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<GetAllCatResponse>>> GetAll([FromQuery] GetAllCat.Request request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut("update")]
        public ActionResult<Task<bool>> Update([FromForm] UpdateCat.Command command)
        {
            return _mediator.Send(command);
        }

        [HttpDelete("delete")]
        public ActionResult<Task> Delete([FromQuery] DeleteCat.Command command)
        {
            return _mediator.Send(command);
        }
    }
}
