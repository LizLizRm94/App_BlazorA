using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppLissy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipolIBROcONTROLLER : ControllerBase
    {

        [HttpGet]
        public IActionResult listarTipoLibro()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
