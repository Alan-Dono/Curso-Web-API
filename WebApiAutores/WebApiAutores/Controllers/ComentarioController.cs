using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/Controllers/{libroId:int}/Comentarios")] // lleva el id porque es una ruta dependiente ya que comentarios depende del libro
    public class ComentarioController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ComentarioController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(int libroId, ComentarioCreacionDTO comentarioCreacionDTO)
        {
            var existeLibro = await context.Libros.AnyAsync(x => x.id == libroId);
            if (!existeLibro)
            {
                return NotFound();
            }
            var comentario = mapper.Map<Comentario>(comentarioCreacionDTO); // destino / fuente
            comentario.libroId = libroId;
            context.Add(comentario);
            await context.SaveChangesAsync();
            var comentarioDTO = mapper.Map<ComentarioDTO>(comentario);
            //return CreatedAtRoute("GetComentario", new { comentarioId = comentario.id, libroId = libroId, }, comentarioDTO);
            return CreatedAtRoute("GetComentario", new { id = comentario.id, libroId = libroId }, comentarioDTO);
        }

        [HttpGet("{id:int}", Name = "GetComentario")]
        public async Task<ActionResult<ComentarioDTO>> GetById (int id)
        {
            var comentario = await context.Comentarios.FirstOrDefaultAsync(comentario => comentario.id == id);
            if (comentario == null) { return NotFound(); }

            return mapper.Map<ComentarioDTO>(comentario);
        } 

        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTO>>> Get(int libroId)
        {
            var existeLibro = await context.Libros.AnyAsync(x => x.id == libroId);
            if (!existeLibro)
            {
                return NotFound();
            }
            var comentarios = await context.Comentarios.Where(x => x.libroId == libroId).ToListAsync();
            return  mapper.Map<List<ComentarioDTO>>(comentarios);
        }
      
    }
}
