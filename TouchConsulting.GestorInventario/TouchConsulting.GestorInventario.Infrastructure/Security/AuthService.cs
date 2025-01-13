using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TouchConsulting.GestorInventario.Application.Interfaces;
using TouchConsulting.GestorInventario.Domain.Entities;

namespace TouchConsulting.GestorInventario.Infrastructure.Security
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string EncriptarSHA256(string texto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public string GenerarJWT(User user)
        {
            try
            {
                //crear la informacion del usuario para token
                var userClaims = new[]
                {
                new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name!)
            };
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                //crear detalle del token
                var jwtConfig = new JwtSecurityToken(
                    issuer: null, // Puedes dejarlo como null o establecerlo según tu configuración
                    audience: null,
                    claims: userClaims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: credentials
                    );

                return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
            }
            catch (Exception ex) { 
                var e = ex.Message;
                return null;
            }
        }
    }
}
