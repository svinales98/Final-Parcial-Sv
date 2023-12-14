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
    public class PersonaController : ControllerBase
    {
        private const string connectionString = "Host=localhost;User Id=postgres;Password=123;Database=optativoIII;";
        private PersonaService PersonaService;

        public PersonaController()
        {
            PersonaService = new PersonaService(connectionString);
        }

        // Busqueda por ID
        [HttpGet("buscarcedula/{cedula}")]
        public IActionResult obtenerPersonaporIdAccion([FromRoute] string cedula)
        {
            var Persona = PersonaService.obtenerPersonaPorCedula(cedula); // Llama al método en el servicio
            return Ok(Persona);
        }
        // POST api/<ValuesController>
        [HttpPost("insertar")]
        public IActionResult insertarPersonaAccion([FromBody] PersonaModel Persona)
        {
            PersonaService.insertarPersona(Persona);
            return Created("El registro fue insertado con exito!", Persona);
        }

        //Listado de registros
        [HttpGet("listado/")]
        public IActionResult obtenerPersonaesAccion()
        {
            var Personas = PersonaService.obtenerPersonas();
            return Ok(Personas);
        }


        // DELETE api/<ValuesController>/5
        [HttpDelete("borrar/{cedula}")]
        public IActionResult eliminarPersonaAccion(string cedula)
        {
            PersonaService.eliminarPersona(cedula);
            return Ok("El registro fue eliminado con exito!");
        }
        // PUT api/<ValuesController>/5
        [HttpPut("modificar/{cedula}")]
        public IActionResult ModificarPersonaAccion([FromBody] PersonaModel Persona)
        {
            PersonaService.modificarPersona(Persona);
            return Ok("El registro fue editado con exito!");
        }
    }
}
