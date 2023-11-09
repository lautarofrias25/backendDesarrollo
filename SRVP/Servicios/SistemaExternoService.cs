using Microsoft.EntityFrameworkCore;
using SRVP.Data;
using SRVP.Data.DTOs.Persona;
using SRVP.Data.DTOs;
using SRVP.Data.Models;

namespace SRVP.Servicios
{
    public class SistemaExternoService
    {
        private readonly SRVPContext _context;

        public SistemaExternoService(SRVPContext context)
        {
            _context = context;
        }
        public async Task<Response<SistemaExterno>> PostSistemaExterno(SistemaExternoDTO sistemaExternoDTO)
        {
            var response = new Response<SistemaExterno>();
            try
            {
                var sistemaExternoDB = await _context.SistemasExternos.FirstOrDefaultAsync(x => x.cuit == sistemaExternoDTO.cuit);
                if (sistemaExternoDB == null)
                {
                    var sistema = new SistemaExterno();
                    sistema.nombre = sistemaExternoDTO.nombre;
                    sistema.cuit = sistemaExternoDTO.cuit;
                    sistema.secreto = sistemaExternoDTO.secreto;

                    _context.SistemasExternos.Add(sistema);
                    _context.SaveChanges();
                    response.Exito = true;
                    response.Mensaje = "La persona se ha creado correctamente";
                    response.Datos = sistema;
                    return (response);
                }
                response.Datos = sistemaExternoDB;
                response.Exito = false;
                response.Mensaje = "El sistema que intenta crear ya existe";
                return (response);
            }
            catch (Exception ex)
            {
                response.Datos = null;
                response.Exito = false;
                response.Mensaje = "No se pudo crear el sistema : " + ex.Message;
                return (response);
            }
        }
    }
}
