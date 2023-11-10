using SRVP.Data.DTOs;
using SRVP.Data.Models;

namespace SRVP.Interfaces
{
    public interface ISistemaExternoService
    {
        Task<Response<SistemaExterno>> PostSistemaExterno(SistemaExternoDTO sistemaExternoDTO)
    }
}
