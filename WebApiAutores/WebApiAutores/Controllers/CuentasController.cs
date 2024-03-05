﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

        public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost("registrar")] // api/cuentas/registrar
        public async Task<ActionResult<RespuestaAutenticacion>> Regisrar(CredencialesUsuario credenciales)
        {
            var user = new IdentityUser { UserName = credenciales.email , Email = credenciales.email };
            var resultado = await userManager.CreateAsync(user, credenciales.password);

            if (resultado.Succeeded)
            {
                return ConstruirToken(credenciales);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }

        private RespuestaAutenticacion ConstruirToken(CredencialesUsuario credencialesUsuario)
        {
            var claim = new List<Claim>()
            {
                new Claim("email",credencialesUsuario.email)
            };
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddDays(1);
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
