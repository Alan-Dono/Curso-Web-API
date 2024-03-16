using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;
using System.Linq;
using System;
using WebApiAutores.Utilidades;
using WebApiAutores.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")] // proteger rutas
    public class AutorController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutorController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet(Name = "obtenerAutores")] // Todos los autores
        [AllowAnonymous] // permitir uso a anonimos
        public async Task<ActionResult<List<AutorLeerDTO>>> Get()
        {
            var autores =  await context.Autores.ToListAsync();
            return mapper.Map<List<AutorLeerDTO>>(autores);
        }

        [HttpGet("{id:int}", Name ="obtenerAutorPorId")] // Autor por id
        public async Task<ActionResult<AutorDTOConLibros>> Get(int id)
        {
            var autor = await context.Autores
                .Include(autorDB => autorDB.AutoresLibros)
                .ThenInclude(libroDB => libroDB.Libro)
                .FirstOrDefaultAsync(x => x.id == id);
            if (autor == null)
            {
                return NotFound();
            }
            return mapper.Map<AutorDTOConLibros>(autor);
        }

        [HttpGet("{nombre}", Name = "obtenerAutorPorNombre")] // Autores por nombre
        public async Task<ActionResult<List<AutorLeerDTO>>> Get([FromRoute] string nombre)
        {
            var autores = await context.Autores.Where(x => x.nombre.Contains(nombre)).ToListAsync();

            return mapper.Map<List<AutorLeerDTO>>(autores);
        }

        [HttpPost(Name ="crearAutor")] // insert
        public async Task<ActionResult> Post(AutorCreacionDTO autorCreacionDTO)
        {
            var ExisteConMismoNombre = await context.Autores.AnyAsync(x => x.nombre == autorCreacionDTO.nombre);
            if (ExisteConMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre {autorCreacionDTO.nombre}");
            }

            var autor = mapper.Map<Autor>(autorCreacionDTO); // destino / fuente

            context.Add(autor);
            await context.SaveChangesAsync();

            var autorDTO = mapper.Map<AutorLeerDTO>(autor);
            return CreatedAtRoute("GetAutor", new {autor.id},autorDTO);
        }
       
        [HttpPut("{id:int}", Name ="actualizarAutor")] // Update por id
        public async Task<ActionResult> Put(AutorLeerDTO autor, int id)
        {
            if (autor.id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la URL");
            }
            var autorActualizado = mapper.Map<Autor>(autor);
            context.Update(autorActualizado);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}", Name ="eliminarAutor")] // Delete
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Autor { id = id });
            await context.SaveChangesAsync();
            return Ok();




        }
    }
}
