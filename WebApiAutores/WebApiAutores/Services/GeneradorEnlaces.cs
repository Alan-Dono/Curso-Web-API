using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using WebApiAutores.DTOs;

namespace WebApiAutores.Services
{
    public class GeneradorEnlaces
    {
        private readonly IAuthorizationService authorizationService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IActionContextAccessor actionContextAccessor;

        public GeneradorEnlaces(IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            IActionContextAccessor actionContextAccessor)
        {
            this.authorizationService = authorizationService;
            this.httpContextAccessor = httpContextAccessor;
            this.actionContextAccessor = actionContextAccessor;
        }

        
        public async Task GenerarEnlaces(AutorLeerDTO autorLeerDTO)
        {
            var esAdmin = await EsAdmin();
            var Url = ConstruirURLHelper();
            autorLeerDTO.Enlaces.Add(new DatoHATEOAS(Url.Link("obtenerAutorPorId", new { id = autorLeerDTO.id }),
                descripcion: "self",
                metodo: "GET"));

            if (esAdmin)
            {
                autorLeerDTO.Enlaces.Add(new DatoHATEOAS(Url.Link("actualizarAutor", new { id = autorLeerDTO.id }),
                descripcion: "autor-actualizar",
                metodo: "PUT"));

                autorLeerDTO.Enlaces.Add(new DatoHATEOAS(Url.Link("eliminarAutor", new { id = autorLeerDTO.id }),
                    descripcion: "autor-eliminar",
                    metodo: "DELETE"));
            }
        }

        private async Task<bool> EsAdmin()
        {
            var httpContex = httpContextAccessor.HttpContext;
            var resultado = await authorizationService.AuthorizeAsync(httpContex.User, "esAdmin");
            return resultado.Succeeded;
        }
        private IUrlHelper ConstruirURLHelper()
        {
            var factoria = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
            return factoria.GetUrlHelper(actionContextAccessor.ActionContext);
        }
    }
}
