using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiAutores.DTOs;

namespace WebApiAutores.Controllers.V1
{
    [ApiController]
    [Route("api/v1")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RootController : ControllerBase
    {
        private readonly IAuthorizationService authorizationService;

        public RootController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }



        [HttpGet(Name = "obtenerRootv1")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DatoHATEOAS>>> Get()
        {
            var esAdmin = await authorizationService.AuthorizeAsync(User, "esAdmin");

            var datoHateoas = new List<DatoHATEOAS>();
            datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("obtenerRoot", new { }),
                descripcion: "self",
                metodo: "GET"));

            datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("obtenerAutores", new { }),
                descripcion: "autores",
                metodo: "GET"));

            if (esAdmin.Succeeded)
            {
                datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("crearAutor", new { }),
                descripcion: "crear-autor",
                metodo: "POST"));

                datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("crearLibro", new { }),
                    descripcion: "crear-libro",
                    metodo: "POST"));
            }

            return datoHateoas;
        }
    }
}
