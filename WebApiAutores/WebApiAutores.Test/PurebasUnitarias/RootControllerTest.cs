using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Authorization;
using WebApiAutores.Controllers.V1;
using WebApiAutores.Test.Mocks;
using Moq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace WebApiAutores.Test.PurebasUnitarias
{
    [TestClass]
    public class RootControllerTest
    {
        [TestMethod]
        public async Task UserIsAdmin_Get4Links()
        {
            // Preparacion
            var authorizationService = new AuthorizationServiceMock();
            authorizationService.Resultado = AuthorizationResult.Success();
            var rootController = new RootController(authorizationService);
            rootController.Url = new URLHelperMock();

            // Ejecucion
            var resultado = await rootController.Get();

            // Verificacion

            Assert.AreEqual(4, resultado.Value.Count());
        }

        [TestMethod]
        public async Task UserNotIsAdmin_Get2Links()
        {
            // Preparacion
            var authorizationService = new AuthorizationServiceMock();
            authorizationService.Resultado = AuthorizationResult.Failed();
            var rootController = new RootController(authorizationService);
            rootController.Url = new URLHelperMock();

            // Ejecucion
            var resultado = await rootController.Get();

            // Verificacion

            Assert.AreEqual(2, resultado.Value.Count());
        }

        [TestMethod]
        public async Task UserNotIsAdmin_Get2Links_ConLibreriaMock()
        {
            // Preparacion
            var mockAuthorization = new Mock<IAuthorizationService>();
            mockAuthorization.Setup(x => x.AuthorizeAsync(
                It.IsAny<ClaimsPrincipal>(),
                It.IsAny<object>(),
                It.IsAny<IEnumerable<IAuthorizationRequirement>>()
                )).Returns(Task.FromResult(AuthorizationResult.Failed()));

            mockAuthorization.Setup(x => x.AuthorizeAsync(
               It.IsAny<ClaimsPrincipal>(),
               It.IsAny<object>(),
               It.IsAny<string>()
               )).Returns(Task.FromResult(AuthorizationResult.Failed()));

            var mokcUrlHelper = new Mock<IUrlHelper>();
            mokcUrlHelper.Setup(x => 
            x.Link(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(string.Empty);

            var rootController = new RootController(mockAuthorization.Object);
            rootController.Url = mokcUrlHelper.Object;

            // Ejecucion
            var resultado = await rootController.Get();

            // Verificacion

            Assert.AreEqual(2, resultado.Value.Count());
        }
    }
}
