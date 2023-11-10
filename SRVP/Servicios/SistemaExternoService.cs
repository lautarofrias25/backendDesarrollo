using Microsoft.EntityFrameworkCore;
using SRVP.Data;
using SRVP.Data.DTOs;
using SRVP.Data.Models;
using Mapster;
using SRVP.Interfaces;

namespace SRVP.Servicios
{
    public class SistemaExternoService : ISistemaExternoService
    {
        private readonly SRVPContext _context;

        public SistemaExternoService(SRVPContext context)
        {
            _context = context;
        }
        public async Task<Response<SistemaExterno>> PostSistemaExterno(SistemaExternoDTO sistemaExternoDTO)
        {
            var response = new Response<SistemaExterno>();
            response.Datos = null;
            response.Exito = false;
            try
            {
                var sistemaExternoDB = await _context.SistemasExternos.AnyAsync(x => x.cuit == sistemaExternoDTO.cuit || x.nombre == sistemaExternoDTO.nombre || x.secreto == sistemaExternoDTO.secreto);
                if (!sistemaExternoDB)
                {
                    var sistema = sistemaExternoDTO.Adapt<SistemaExterno>();
                    
                    await _context.SistemasExternos.AddAsync(sistema);
                    await _context.SaveChangesAsync();
                    response.Exito = true;
                    response.Mensaje = "El sistema se ha creado correctamente";
                    response.Datos = sistema;
                    return (response);
                }              
                response.Mensaje = "El sistema que intenta crear ya existe";
                return (response);
            }
            catch (Exception ex)
            {  
                response.Mensaje = "Error interno: " + ex.Message;
                return (response);
            }
        }
    }
}
