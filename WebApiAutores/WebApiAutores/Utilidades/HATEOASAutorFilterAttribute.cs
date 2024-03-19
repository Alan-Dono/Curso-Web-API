using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiAutores.DTOs;
using WebApiAutores.Services;

namespace WebApiAutores.Utilidades
{
    public class HATEOASAutorFilterAttribute: HateoasFiltroAtribute
    {
        private readonly GeneradorEnlaces generadorEnlaces;

        public HATEOASAutorFilterAttribute(GeneradorEnlaces generadorEnlaces)
        {
            this.generadorEnlaces = generadorEnlaces;
        }
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var incluirHATEOAS = IncluirHATEOAS(context);
            if (!incluirHATEOAS)
            {
                await next();
                return;
            }
            var resultado = context.Result as ObjectResult;
            var modelo = resultado.Value as AutorLeerDTO ?? throw new ArgumentException("Se esperaba una instancia de AutorLeerDTO");
            await generadorEnlaces.GenerarEnlaces(modelo);
            await next();
        }
    }
}
