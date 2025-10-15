using AppBlazor.Entities;
using BlazorAppLissy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlazorAppLissy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly BdbibliotecaContext bd;
        public AutorController(BdbibliotecaContext _bd)
        {
            this.bd = _bd;
        }
        [HttpGet]
        public IActionResult listarAutor()
        {
            try
            {
                var lista = (from autor in this.bd.Autors
                             where autor.Bhabilitado == 1
                             select new AutorCLS
                             {
                                 idautor = autor.Iidautor,
                                 nombreautor = autor.Nombre + " " + autor.Appaterno + " " + autor.Apmaterno
                             }).ToList();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
