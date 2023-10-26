using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_de_registro_y_validacion_de_personas.Data.DTOs.Persona;
using SRVP.Data;
using SRVP.Data.DTOs;
using System.Runtime.CompilerServices;

namespace SRVP.Servicios
{
    public class PersonaService
    {
        private readonly SRVPContext _context;
        //NO SE POR QUE ESTA ACA
        //private string nombre;
        //private string apellido;
        //private int cuil;
        //private int dni;
        //private string genero;
        //private int telefono;
        //private bool habilitado;
        //private bool estado;
        //private bool estadoCrediticio;
        //private object personaDTO;

        public PersonaService(SRVPContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<ICollection<PersonaDTO>>> GetPersonas()
        {
            var personas = await _context.Personas.ToListAsync();
            var personasDTO = new List<PersonaDTO>();
            foreach (var persona in personas)
            {
                var personaDTO = new PersonaDTO();
                personaDTO.id = persona.id;
                personaDTO.nombre = persona.nombre;
                personaDTO.apellido = persona.apellido;
                personaDTO.cuil = persona.cuil;
                personaDTO.dni = persona.dni;
                personaDTO.fechaNacimiento = persona.fechaNacimiento; //VER
                personaDTO.genero = persona.genero;
                personaDTO.telefono = persona.telefono;
                personaDTO.habilitado = persona.habilitado;
                personaDTO.estado = persona.estado;
                personaDTO.estadoCrediticio = persona.estadoCrediticio;
            }
            return (personasDTO);
        }

        public async Task<ActionResult<PersonaDTO>> GetPersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            var personaDTO = new PersonaDTO
            { 
                    id = persona.id,
                    nombre = persona.nombre,
                    apellido = persona.apellido,
                    cuil = persona.cuil,
                    dni = persona.dni,
                    fechaNacimiento = persona.fechaNacimiento,
                    genero = persona.genero,
                    telefono = persona.telefono,
                    habilitado = persona.habilitado,
                    estado = persona.estado,
                    estadoCrediticio = persona.estadoCrediticio
            };
            return (personaDTO);
        }

        public async Task<ActionResult<Persona>> PostPersona([FromBody] CreatePersonaDTO personaDTO)
        {
            var persona = await _context.Personas.FindAsync(personaDTO.cuil);
            if (persona != null)
            {
                persona.nombre = personaDTO.nombre;
                persona.apellido = personaDTO.apellido;
                persona.cuil = personaDTO.cuil;
                persona.dni = personaDTO.dni;
                persona.fechaNacimiento = personaDTO.fechaNacimiento; //VER
                persona.genero = personaDTO.genero;
                persona.telefono = personaDTO.telefono;
                persona.habilitado = personaDTO.habilitado;
                persona.estado = personaDTO.estado;
                persona.estadoCrediticio = personaDTO.estadoCrediticio;
                _context.Personas.Add(persona);
                _context.SaveChanges();
            }
            else
            {
                persona = null;
            }
            return (persona);
        }

        public async Task<ActionResult<Persona>> PutPersona([FromBody] PersonaDTO personaDTO)
        {
            var personaBD = await _context.Personas.FindAsync(personaDTO.id);
            if (personaBD != null)
            {
                personaBD.nombre = personaDTO.nombre;
                personaBD.apellido = personaDTO.apellido;
                personaBD.cuil = personaDTO.cuil;
                personaBD.dni = personaDTO.dni;
                personaBD.fechaNacimiento = personaDTO.fechaNacimiento; //VER
                personaBD.genero = personaDTO.genero;
                personaBD.telefono = personaDTO.telefono;
                personaBD.habilitado = personaDTO.habilitado;
                personaBD.estado = personaDTO.estado;
                personaBD.estadoCrediticio = personaDTO.estadoCrediticio;
                await _context.SaveChangesAsync();
            }
            return (personaBD);
        }

        public async Task<ActionResult<Persona>> DeletePersona(int id)
        {
            var personaBD = await _context.Personas.FindAsync(id);
            if (personaBD == null)
            {
                _context.Personas.Remove(personaBD);
                await _context.SaveChangesAsync();
            }

            return (personaBD);
        }
    }
}

