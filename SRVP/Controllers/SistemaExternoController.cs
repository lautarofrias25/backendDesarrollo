using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SRVP.Data.DTOs.Persona;
using SRVP.Data.DTOs;
using SRVP.Data.Models;
using SRVP.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SRVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SistemaExternoController : ControllerBase
    {
        private readonly ISistemaExternoService _service;

         public SistemaExternoController(ISistemaExternoService service)
         {
            _service = service;
         }

        // POST: SistemaExternoController/Create
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Response<SistemaExterno>>> PostSistemaExterno([FromBody] SistemaExternoDTO sistemaExternoDTO)
        {
            var response = await _service.PostSistemaExterno(sistemaExternoDTO);
            if (response.Datos == null)
            {
                if(response.Mensaje.StartsWith("Error interno"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
