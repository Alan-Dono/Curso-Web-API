using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route ("api/autores")]
    public class AutorController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AutorController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Autor>> Post(Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        } 
    }
}
