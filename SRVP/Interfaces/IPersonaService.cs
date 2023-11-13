using SRVP.Data.DTOs;
using SRVP.Data.DTOs.Persona;
using SRVP.Data.Models;

namespace SRVP.Interfaces;

public interface IPersonaService
{
    Task<Response<ICollection<PersonaDTO>>> GetPersonas();
    Task<Response<PersonaDTO>> GetPersona(int id);
    Task<Response<bool>> GetEstadoCrediticio(int id);
    Task<Response<Persona>> PostPersona(RegisterPersonaDTO personaDTO);
    Task<Response<Persona>> PutPersona(PersonaDTO personaDTO);
    Task<Response<ICollection<PersonaDTO>>> PutEstadosCrediticios();
    Task<Response<Persona>> DeletePersona(int id);

}