using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SRVP.Data.DTOs;
using SRVP.Data.DTOs.Persona;
using SRVP.Data.Models;
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
        public async Task<ActionResult<Response<ICollection<PersonaDTO>>>> GetPersonasAsync()
        {
            var response = await _service.GetPersonas();
            if (response.Datos == null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        // GET: PersonaController/GetPersona/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<PersonaDTO>>> GetPersona(int id)
        {
            var response = await _service.GetPersona(id);
            if (response.Datos == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // POST: PersonaController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<Response<Persona>>> PostPersona([FromBody] CreatePersonaDTO personaDTO)
        {
            var response = await _service.PostPersona(personaDTO);
            if (response.Datos == null)
            {
                return BadRequest(response);
            }
            return Ok(response);            
        }

        // PUT: PersonaController/PutPersona/5
        [HttpPut]
        public async Task<ActionResult<Response<Persona>>> PutPersona([FromBody] PersonaDTO personaDTO)
        {
            var response = await _service.PutPersona(personaDTO);
            if (response.Datos == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
            //falta
        }

        // DELETE: PersonaController/DeletePersona/5
        public async Task<ActionResult<Response<Persona>>> DeletePersona(int id)
        {
            var response = await _service.DeletePersona(id);
            if (response.Datos == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
