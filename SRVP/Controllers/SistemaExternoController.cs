using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SRVP.Data.DTOs.Persona;
using SRVP.Data.DTOs;
using SRVP.Data.Models;
using SRVP.Servicios;

namespace SRVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SistemaExternoController : ControllerBase
    {
        private readonly SistemaExternoService _service;

         public SistemaExternoController(SistemaExternoService service)
         {
            _service = service;
         }

        // POST: SistemaExternoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Response<SistemaExterno>>> PostSistemaExterno([FromBody] SistemaExternoDTO sistemaExternoDTO)
        {
            var response = await _service.PostSistemaExterno(sistemaExternoDTO);
            if (response.Datos == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
