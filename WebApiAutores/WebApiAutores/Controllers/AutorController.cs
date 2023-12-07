using Microsoft.AspNetCore.Mvc;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route ("api/autores")]
    public class AutorController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Autor>> Get()
        {
            return new List<Autor>()
            {
                new Autor () {id =1, nombre = "Dono Alana"} ,
                new Autor () {id = 2, nombre ="Lopez Martin"}
            };
        }
    }
}
