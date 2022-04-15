using Common.MCat;
using Common.MCat.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EffectiveCats.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CatController : BaseController
    {
        public CatController()
        {
            
        }

        [HttpPost("create")]
        public async Task<ActionResult<long>> Create([FromForm] CreateCat.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("getById")]
        public async Task<ActionResult<GetByIdCatDto>> GetById([FromQuery] GetByIdCat.Request request)
        {
            return await Mediator.Send(request);
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<GetAllCatDto>>> GetAll([FromQuery] GetAllCat.Request request)
        {
            return await Mediator.Send(request);
        }

        [HttpPut("update")]
        public async Task<ActionResult<Cat>> Update([FromForm] UpdateCat.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<long>> Delete([FromQuery] DeleteCat.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
