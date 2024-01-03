using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;
using System;
using WebApiAutores.Utilidades;
using WebApiAutores.DTOs;
using AutoMapper;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutorController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutorController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Autor>> Post(AutorCreacionDTO autorCreacionDTO)
        {
            var ExisteConMismoNombre = await context.Autores.AnyAsync(x => x.nombre == autorCreacionDTO.nombre);
            if (ExisteConMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre {autorCreacionDTO.nombre}");
            }

            var autor = mapper.Map<Autor>(autorCreacionDTO); // destino / fuente

            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.id != id)
            {
                return BadRequest("El id del autor no coincide con el id de l URL");
            }
            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
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
