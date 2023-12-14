using Microsoft.AspNetCore.Mvc;
using Infra.Modelos;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Service.Service;
using Microsoft.AspNetCore.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace optativoIIIFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private const string connectionString = "Host=localhost;User Id=postgres;Password=123;Database=optativoIII;";
        private UsuarioService usuarioService;

        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
            usuarioService = new UsuarioService(connectionString);
        }
        // POST api/<TokenController>
        [HttpPost("login")]
        public IActionResult Post([FromBody] Login login)
        {
            int intentos = 1;
            var userIsValid = validUser(login);
                if (!userIsValid)
                {
                    return Unauthorized();
                }
                var datousu = usuarioService.obtenerusuarioPorNombre(login.Username);
                var token = GenerateJWT(datousu.persona.nombre, datousu.persona.apellido, datousu.persona.email);
                return Ok(token);
        }
        private object GenerateJWT(string nombre,string apellido,string correo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                 new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, nombre),
                 new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, apellido),
                 new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, correo),
                 new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddSeconds(320),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private bool validUser(Login login)
        {
            var usuario = usuarioService.obtenerusuarioPorNombre(login.Username);
            if (usuario.estado == "A")
            {
                return login.Username == usuario.nombreUsuario && login.Password == usuario.contrasena;
            }
            return false;
        }
    }
}
