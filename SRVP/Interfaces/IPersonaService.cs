using SRVP.Data.DTOs;
using SRVP.Data.DTOs.Persona;
using SRVP.Data.Models;
using SRVP.DTOs.Persona;

namespace SRVP.Interfaces;

public interface IPersonaService
{
    Task<Response<ICollection<PersonaDTO>>> GetPersonas();
    Task<Response<PersonaDTO>> GetPersona(int id);
    Task<Response<bool?>> GetEstadoCrediticio(int id);
    Task<Response<PersonaDTO>> PostPersona(RegisterPersonaDTO personaDTO);
    Task<Response<PersonaDTO>> PutPersona(PutPersonaDTO personaDTO);
    Task<Response<ICollection<PersonaDTO>>> PatchEstadosCrediticios();
    Task<Response<PersonaDTO>> PatchEstadoCrediticio(int cuil, bool nuevoEstado);
    Task<Response<Persona>> DeletePersona(int id);

}