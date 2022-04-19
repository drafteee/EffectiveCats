using Common.MCat;
using Common.MCat.Dto;
using DAL.Models;
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
        public ActionResult<Task<GetByIdCatDto>> GetById([FromQuery] GetByIdCat.Request request)
        {
            return _mediator.Send(request);
        }

        [HttpGet("getAll")]
        public ActionResult<Task<List<GetAllCatDto>>> GetAll([FromQuery] GetAllCat.Request request)
        {
            return _mediator.Send(request);
        }

        [HttpPut("update")]
        public ActionResult<Task<Cat>> Update([FromForm] UpdateCat.Command command)
        {
            return _mediator.Send(command);
        }

        [HttpDelete("delete")]
        public ActionResult<Task<long>> Delete([FromQuery] DeleteCat.Command command)
        {
            return _mediator.Send(command);
        }
    }
}
