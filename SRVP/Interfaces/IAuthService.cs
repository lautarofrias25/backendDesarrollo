using SRVP.Data.DTOs;
using SRVP.Data.DTOs.Persona;
using SRVP.Data.Models;
using SRVP.DTOs;
using SRVP.DTOs.Persona;

namespace SRVP.Interfaces
{
    public interface IAuthService
    {
        Task<Response<string>> loguearAccessCode(PersonaLoginDto user);
        Task<Response<Persona>> registrarPersona(RegisterPersonaDTO persona);
        Task<Response<string>> loguearJWT(LoginJWTDto request);
        Task<Response<RespuestaLogin>> loguearInterno(LoginInternoDto user);

    }
}
