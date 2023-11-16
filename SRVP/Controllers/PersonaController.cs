using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SRVP.Data.DTOs;
using SRVP.Data.DTOs.Persona;
using SRVP.Data.Models;
using SRVP.DTOs.Persona;
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
        [HttpGet("getPersonas")]
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
        [HttpGet("getPersona{id}")]
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
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<Response<PersonaDTO>>> PostPersona([FromBody] RegisterPersonaDTO personaDTO)
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
        [HttpPut]
        public async Task<ActionResult<Response<PersonaDTO>>> PutPersona([FromBody] PutPersonaDTO personaDTO)
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

        //ok, revisar si esta bien. 
        
        //GET PersonaController/GetEstadoCrediticio/5
        [HttpGet("getEstadoCrediticio{cuit}")]
        public async Task<ActionResult<Response<bool?>>> GetEstadoCrediticio(int cuil)
        {
            var response = await _service.GetEstadoCrediticio(cuil);
            if (response.Datos == null) //no se si esta bien
            {
                if (response.Mensaje.StartsWith("Error interno"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }
                return NotFound(response);
            }
            return Ok(response);
        }

        // PUT: PersonaController/PutEstadosCrediticios
        [HttpPatch("PatchEstadosCrediticios")] //REVISAR
        public async Task<ActionResult<Response<bool>>> PatchEstadosCrediticios()
        {
            var response = await _service.PatchEstadosCrediticios();
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

        [HttpPatch("PatchEstadoCrediticio")]
        public async Task<ActionResult<Response<PersonaDTO>>> PatchEstadoCrediticio(int cuil, bool nuevoEstado)
        {
            var response = await _service.PatchEstadoCrediticio(cuil, nuevoEstado);
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
    }
}
