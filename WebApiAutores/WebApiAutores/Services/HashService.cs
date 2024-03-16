using System.Globalization;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using WebApiAutores.DTOs;

namespace WebApiAutores.Services
{
    public class HashService
    {
        public ResultadoHash Hash(string textoPlano)
        {
            var sal = new byte[16];
            using ( var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(sal);
            }
            return Hash(textoPlano, sal);
        }

        public ResultadoHash Hash(string textoPlano, byte[] sal)
        {
            var llaverDerivada = KeyDerivation.Pbkdf2(password: textoPlano,
                salt: sal,
                KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 32);
            
            var hash = Convert.ToBase64String(llaverDerivada);
            return new ResultadoHash()
            {
                Hash = hash,
                Sal = sal
            };
        }
    }
}
