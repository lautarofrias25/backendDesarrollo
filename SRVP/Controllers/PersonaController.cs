using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SRVP.Data.DTOs;
using SRVP.Data.DTOs.Persona;
using SRVP.Data.Models;
using SRVP.Interfaces;
using SRVP.Servicios;

namespace SRVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService _service;

        public PersonaController(IPersonaService service)
        {
            _service = service;
        }

        // GET: PersonaController
        [Authorize(AuthenticationSchemes = "SymmetricScheme, AsymmetricScheme")]
        [HttpGet]
        public async Task<ActionResult<Response<ICollection<PersonaDTO>>>> GetPersonasAsync()
        {
            var response = await _service.GetPersonas();
            if (response.Datos == null)
            {
                if (response.Mensaje.StartsWith("Error interno"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }
                return BadRequest(response);
            }
            return Ok(response);
        }

        // GET: PersonaController/GetPersona/5
        [Authorize(AuthenticationSchemes = "SymmetricScheme, AsymmetricScheme")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<PersonaDTO>>> GetPersona(int id)
        {
            var response = await _service.GetPersona(id);
            if (response.Datos == null)
            {
                if (response.Mensaje.StartsWith("Error interno"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: PersonaController/Create
        [Authorize(AuthenticationSchemes = "SymmetricScheme")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<Response<Persona>>> PostPersona([FromBody] RegisterPersonaDTO personaDTO)
        {
            var response = await _service.PostPersona(personaDTO);
            if (response.Datos == null)
            {
                if (response.Mensaje.StartsWith("Error interno"))                               //Redundante, la persona se crea cuando se 
                {                                                                               //crea cuando se registra
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }
                return BadRequest(response);
            }
            return StatusCode(StatusCodes.Status201Created, response);            
        }

        // PUT: PersonaController/PutPersona/5
        [Authorize(AuthenticationSchemes = "SymmetricScheme")]
        [HttpPut]
        public async Task<ActionResult<Response<Persona>>> PutPersona([FromBody] PersonaDTO personaDTO)
        {
            var response = await _service.PutPersona(personaDTO);
            if (response.Datos == null)
            {
                if (response.Mensaje.StartsWith("Error interno"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }
                return BadRequest(response);
            }
            return Ok(response);
            //falta
        }

        // DELETE: PersonaController/DeletePersona/5
        [Authorize(AuthenticationSchemes = "SymmetricScheme")]
        [HttpDelete]
        public async Task<ActionResult<Response<Persona>>> DeletePersona(int id)
        {
            var response = await _service.DeletePersona(id);
            if (response.Datos == null)
            {
                if (response.Mensaje.StartsWith("Error interno"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }
                return NotFound(response);
            }
            return Ok(response);
        }
        // Falta un get que devuelva el estado crediticio de un usuario
        // un put que cambie todos los estados crediticios
    }
}
