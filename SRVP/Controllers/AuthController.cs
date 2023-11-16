using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SRVP.Data.DTOs;
using SRVP.Data.DTOs.Persona;
using SRVP.Data.Models;
using SRVP.DTOs;
using SRVP.DTOs.Persona;
using SRVP.Interfaces;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SRVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController (IAuthService authService)
        {
            _authService = authService;
        }
        // POST api/<AuthController> redireccionado
        [AllowAnonymous]
        [HttpPost("loguearAccessCode")]
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
        [AllowAnonymous]
        [HttpPost("registrarPersona")]
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
            return StatusCode(StatusCodes.Status201Created, response);            
        }

        [AllowAnonymous]
        [HttpPost("loguearJWT")]
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

        [AllowAnonymous]
        [HttpPost("loguearInterno")]
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
