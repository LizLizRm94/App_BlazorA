using Microsoft.AspNetCore.Mvc;

namespace BlazorAppLissy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        [HttpGet]
        public IActionResult listarLibros()
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

        [HttpGet("{idlibro}")]
        public IActionResult recuperarLibroPorId(int idlibro)
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


        [HttpDelete("{idlibro}")]
        public IActionResult eliminarlibro(int idlibro)
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
