using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_de_registro_y_validacion_de_personas.Data.DTOs.Persona;
using SRVP.Data;
using SRVP.Data.DTOs;
using SRVP.Servicios;

namespace SRVP.Controllers
{
    [Route("api/[controller")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly PersonaService _service;

        public PersonaController(PersonaService service)
        {
            _service = service;
        }

        // GET: PersonaController
        [HttpGet]
        public async Task<ActionResult<ICollection<PersonaDTO>>> GetPersonasAsync()
        {
            var personas = await _service.GetPersonas();
            if (personas == null)
            {
                return NoContent();
            }
            return Ok(personas);
        }

        // GET: PersonaController/GetPersona/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaDTO>> GetPersona(int id)
        {
            var persona = await _service.GetPersona(id);
            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }

        // POST: PersonaController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<Persona>> PostPersona([FromBody] CreatePersonaDTO personaDTO)
        {
            var persona = await _service.PostPersona(personaDTO);
            if (persona == null)
            {
                return NotFound(persona);
            }
            else
            {
                return Ok(persona);
            }            
        }

        // PUT: PersonaController/PutPersona/5
        public async Task<ActionResult<Persona>> PutPersona([FromBody] PersonaDTO personaDTO)
        {
            var personaMod = await _service.PutPersona(personaDTO);
            return Ok();
            //falta
        }

        // DELETE: PersonaController/DeletePersona/5
        public async Task<ActionResult<Persona>> DeletePersona(int id)
        {
            var persona = await _service.DeletePersona(id);
            if (persona == null)
            {
                return NotFound(persona);
            }
            return Ok(persona);
        }
    }
}
