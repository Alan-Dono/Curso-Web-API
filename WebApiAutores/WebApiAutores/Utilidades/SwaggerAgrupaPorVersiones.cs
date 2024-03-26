using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebApiAutores.Utilidades
{
    public class SwaggerAgrupaPorVersiones : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var nameSpaceControlador = controller.ControllerType.Namespace; // controller/V1
            var versionApi = nameSpaceControlador.Split('.').Last().ToLower(); // v1
            controller.ApiExplorer.GroupName = versionApi;
        }
    }
}
