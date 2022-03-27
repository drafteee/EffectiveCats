using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace EffectiveCats.Controllers
{
    [Route("api/[controller]")]
    public class CatController : Controller
    {
        private readonly CatService _service;

        public CatController(CatService service)
        {
            _service = service;
        }

        [HttpGet("GetCat")]
        public async Task<ActionResult<bool>> GetCat()
        {
            try
            {
                _service.GetAll();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
