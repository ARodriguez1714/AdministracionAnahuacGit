using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ML;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticationController : ControllerBase
    {
        private readonly string secretKey;

        //Inyección de Dependencias
        public AutenticationController(IConfiguration configuracion)
        {
            secretKey = configuracion.GetSection("jwt").GetSection("secretKey").ToString();
        }

        [HttpPost]
        [Route("validar")]
        public IActionResult Validar([FromBody] UserIdentity request)
        {
            ML.Result result = BL.UserIdentity.GetByEmail(request.Email);
            var user = (ML.UserIdentity)result.Object;
            if (request.Email == user.Email && request.Password == user.Password)
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim (ClaimTypes.NameIdentifier, request.UserName));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddSeconds(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new {token = tokenCreado});
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "Error." });
            }
        }
    }
}
