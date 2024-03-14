using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiAutores.DTOs;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager )
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost("registrar")] // api/cuentas/registrar
        public async Task<ActionResult<RespuestaAutenticacion>> Regisrar(CredencialesUsuario credenciales)
        {
            var user = new IdentityUser { UserName = credenciales.Email , Email = credenciales.Email };
            var resultado = await userManager.CreateAsync(user, credenciales.Password);

            if (resultado.Succeeded)
            {
                return ConstruirToken(credenciales);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credencialesUsuario)
        {
            var resultado = await signInManager.PasswordSignInAsync(credencialesUsuario.Email, credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }

        [HttpGet("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<RespuestaAutenticacion> RenovarToken()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "Email").FirstOrDefault();
            var email = emailClaim.Value;
            var credencialesUsuario = new CredencialesUsuario()
            {
                Email = email,
            };
            return ConstruirToken(credencialesUsuario);
        }


  
        private RespuestaAutenticacion ConstruirToken(CredencialesUsuario credencialesUsuario)
        {
            var claim = new List<Claim>()
            {
                new Claim("Email",credencialesUsuario.Email)
            };
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddMinutes(30);
            var securityToken = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claim,
                expires: expiracion,
                signingCredentials: creds
                );
            return new RespuestaAutenticacion()
            {
                token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                expiracion = expiracion
            };
        }
    }
}
