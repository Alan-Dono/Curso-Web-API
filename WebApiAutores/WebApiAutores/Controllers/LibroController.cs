using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibroController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public LibroController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            return await context.Libros.Include(x => x.autor ).FirstOrDefaultAsync(x => x.idAutor == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existe = await context.Autores.AnyAsync(x => x.id == libro.idAutor);
            if (!existe)
            {
                return BadRequest($"No existe el autor de id {libro.idAutor}");
            }
            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();    

        }
    }
}
