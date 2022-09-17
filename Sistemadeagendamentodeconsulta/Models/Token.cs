using System;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Sistemadeagendamentodeconsulta.Models
{
    public static class Token
    {
        public static string GenerateToken(Usuario usuario)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["Secret"]);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nome.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
