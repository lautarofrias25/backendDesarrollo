using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SRVP.Data;
using SRVP.Data.DTOs;
using SRVP.Data.DTOs.Persona;
using SRVP.Data.Models;
using SRVP.DTOs;
using SRVP.DTOs.Persona;
using SRVP.Helpers;
using SRVP.Models;
using SRVP.Servicios;
using System.Text;
using System.Xml;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SRVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController (AuthService authService)
        {
            _authService = authService;
        }
        // POST api/<AuthController>
        [HttpPost]
        public async Task<ActionResult<Response<string>>> loguearAccessCode([FromBody] PersonaLoginDto user)
        {
            var response = await _authService.loguearAccessCode(user);
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

        // PUT api/<AuthController>/5
        [HttpPost]
        public async Task<ActionResult<Response<Persona>>> registrarPersona([FromBody] RegisterPersonaDTO persona)
        {
            var response = await _authService.registrarPersona(persona);
            if (response.Datos == null)
            {
                if (response.Mensaje.StartsWith("Error interno"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }
                return BadRequest(response);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, response);            
        }

        [HttpPost]
        public async Task<ActionResult<Response<string>>> loguearJWT([FromBody] LoginJWTDto request)
        {
            var response = await _authService.loguearJWT(request);
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
        [HttpPost]
        public async Task<ActionResult<Response<RespuestaLogin>>> loguearInterno([FromBody] LoginInternoDto user)
        {
            var response = await _authService.loguearInterno(user);
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

        //To do Post para login interno done
    }
}
