using MediatR.MCat;
using MediatR.Cat.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EffectiveCats.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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

        [HttpGet("getById")]
        public ActionResult<Task<GetByIdCatResponse>> GetById([FromQuery] GetByIdCat.Request request)
        {
            return _mediator.Send(request);
        }

        [HttpGet("getAll")]
        public ActionResult<Task<List<GetAllCatResponse>>> GetAll([FromQuery] GetAllCat.Request request)
        {
            return _mediator.Send(request);
        }

        [HttpPut("update")]
        public ActionResult<Task<bool>> Update([FromForm] UpdateCat.Command command)
        {
            return _mediator.Send(command);
        }

        [HttpDelete("delete")]
        public ActionResult<Task<bool>> Delete([FromQuery] DeleteCat.Command command)
        {
            return _mediator.Send(command);
        }
    }
}
