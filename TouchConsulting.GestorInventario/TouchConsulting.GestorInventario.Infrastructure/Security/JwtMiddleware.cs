using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace TouchConsulting.GestorInventario.Infrastructure.Security
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _secretKey;

        public JwtMiddleware(RequestDelegate next, string secretKey)
        {
            _next = next;
            _secretKey = secretKey;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_secretKey);

                try
                {
                    // Validación del token
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);
                }
                catch
                {
                    // Si el token es inválido, devolver un código de estado 401
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var responseObj = new
                    {
                        isSuccess = false,
                        message = "Token no autorizado"
                    };

                    var responseJson = JsonSerializer.Serialize(responseObj);
                    await context.Response.WriteAsync(responseJson);
                    return;
                }
            }

            // Si no hay token o el token es válido, continuar con la solicitud
            await _next(context);
        }
    }
}
