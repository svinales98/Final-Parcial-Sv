using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Infra.Modelos;
using Service.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace optativoIIIFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private const string connectionString = "Host=localhost;User Id=postgres;Password=123;Database=optativoIII;";
        private UsuarioService usuarioService;

        public UsuarioController()
        {
            usuarioService = new UsuarioService(connectionString);
        }

        // Busqueda por ID
        [HttpGet("buscarusuario/{id}")]
        public IActionResult obtenerUsuarioporIdAccion([FromRoute] int id)
        {
            var usuario = usuarioService.obtenerusuarioPorId(id); // Llama al método en el servicio
            return Ok(usuario);
        }

        // POST api/<ValuesController>
        [HttpPost("insertar")]
        public IActionResult insertarUsuarioAccion([FromBody] UsuarioModel usuario)
        {
            usuarioService.insertarUsuario(usuario);
            return Created("El regsitro fue insertado con exito!", usuario);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("modificar/{id}")]
        public IActionResult ModificarUsuarioAccion([FromBody] UsuarioModel usuario)
        {
            usuarioService.modificarUsuario(usuario);
            return Ok("El registro fue editado con exito!");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("borrar/{id}")]
        public IActionResult eliminarUsuarioAccion(int id)
        {
            usuarioService.eliminarUsuario(id);
            return Ok("El registro fue eliminado con exito!");
        }
    }
}
