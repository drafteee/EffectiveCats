using MediatR.TypeCat.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR.MTypeCat;

namespace EffectiveCats.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CatTypeController : ControllerBase
    {
        private IMediator _mediator;

        public CatTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public ActionResult<Task<long>> Create([FromForm] CreateCatType.Command command)
        {
            return _mediator.Send(command);
        }

        [HttpGet("getById")]
        public ActionResult<Task<GetByIdTypeCatResponse>> GetById([FromQuery] GetByIdCatType.Request request)
        {
            return _mediator.Send(request);
        }

        [HttpGet("getAll")]
        public ActionResult<Task<List<GetAllTypeCatResponse>>> GetAll([FromQuery] GetAllCatType.Request request)
        {
            return _mediator.Send(request);
        }

        [HttpPut("update")]
        public ActionResult<Task<bool>> Update([FromForm] UpdateCatType.Command command)
        {
            return _mediator.Send(command);
        }

        [HttpDelete("delete")]
        public ActionResult<Task<bool>> Delete([FromQuery] DeleteCatType.Command command)
        {
            return _mediator.Send(command);
        }
    }
}
