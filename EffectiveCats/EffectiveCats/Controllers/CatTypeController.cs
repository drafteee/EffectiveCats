using Common.MTypeCat;
using Common.MTypeCat.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EffectiveCats.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CatTypeController : BaseController
    {
        public CatTypeController()
        {

        }

        [HttpPost("create")]
        public async Task<ActionResult<bool>> Create([FromForm] CreateCatType.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("getById")]
        public async Task<ActionResult<GetByIdTypeCatDto>> GetById([FromQuery] GetByIdCatType.Request request)
        {
            return await Mediator.Send(request);
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<GetAllTypeCatDto>>> GetAll([FromQuery] GetAllCatType.Request request)
        {
            return await Mediator.Send(request);
        }

        [HttpPut("update")]
        public async Task<ActionResult<CatType>> Update([FromForm] UpdateCatType.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<long>> Delete([FromQuery] DeleteCatType.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
