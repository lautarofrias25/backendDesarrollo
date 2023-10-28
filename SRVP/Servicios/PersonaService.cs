using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SRVP.Data.DTOs.Persona;
using SRVP.Data;
using System.Runtime.CompilerServices;
using SRVP.Data.Models;
using SRVP.Data.DTOs;

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

        public async Task<Response<ICollection<PersonaDTO>>> GetPersonas()
        {
            var response = new Response<ICollection<PersonaDTO>>();
            try
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
                response.Datos = personasDTO;
                if (personasDTO == null)
                {
                    response.Exito = false;
                    response.Mensaje = "No se hallaron personas";
                }
                else
                {
                    response.Exito = true;
                    response.Mensaje = "Se recuperaron correctamente todas las personas";
                }
                return (response);
            }
            catch (Exception ex)
            {
                response.Datos = null;
                response.Exito = false;
                response.Mensaje = "No se pudieron recuperar las personas : " + ex.Message;
                return (response);
            }
        }

        public async Task<Response<PersonaDTO>> GetPersona(int id)
        {
            var response = new Response<PersonaDTO>();
            try
            {
                var persona = await _context.Personas.FirstOrDefaultAsync(x => x.id == id);
                if (persona != null)
                {
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
                    response.Datos = personaDTO;
                    response.Exito = true;
                    response.Mensaje = "Se recupero correctamente la persona";
                    return (response);
                };
                response.Exito = false;
                response.Mensaje = "No se hallo la persona";
                response.Datos = null;
                return (response);
            }
            catch (Exception ex)
            {
                response.Datos = null;
                response.Exito = false;
                response.Mensaje = "No se pudo recuperar la persona : " + ex.Message;
                return (response);
            }
        }

        public async Task<Response<Persona>> PostPersona(CreatePersonaDTO personaDTO)
        {
            var response = new Response<Persona>();
            try
            {
                var personaDB = await _context.Personas.FirstOrDefaultAsync(x => x.cuil == personaDTO.cuil);
                if (personaDB == null)
                {
                    var persona = new Persona();
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
                    response.Exito = true;
                    response.Mensaje = "La persona se ha creado correctamente";
                    response.Datos = persona;
                    return (response);
                }
                response.Datos = personaDB;
                response.Exito = false;
                response.Mensaje = "La persona que intenta crear ya existe";
                return (response);
            }
            catch (Exception ex)
            {
                response.Datos = null;
                response.Exito = false;
                response.Mensaje = "No se pudo crear la persona : " + ex.Message;
                return (response);
            }
        }

        public async Task<Response<Persona>> PutPersona(PersonaDTO personaDTO)
        {
            var response = new Response<Persona>();
            try
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
                    response.Datos = personaBD;
                    response.Exito = true;
                    response.Mensaje = "La persona se ha modificado correctamente";
                    return response;
                }
                response.Datos = null;
                response.Exito = false;
                response.Mensaje = "La persona a modificar no fue hallada";
                return (response);
            }
            catch (Exception ex)
            {
                response.Datos = null;
                response.Exito = false;
                response.Mensaje = "No se pudo modificar la persona : " + ex.Message;
                return response;
            }
        }

        public async Task<Response<Persona>> DeletePersona(int id)
        {
            var response = new Response<Persona>();
            try
            {
                var personaBD = await _context.Personas.FindAsync(id);
                if (personaBD != null)
                {
                    _context.Personas.Remove(personaBD);
                    await _context.SaveChangesAsync();
                    response.Datos = personaBD;
                    response.Exito = true;
                    response.Mensaje = "La persona se ha eliminado correctamente ";
                    return response;
                }
                response.Datos = null;
                response.Exito = false;
                response.Mensaje = "La persona a eliminar no fue hallada ";
                return (response);
            }
            catch (Exception ex)
            {
                response.Datos = null;
                response.Exito = false;
                response.Mensaje = "No se pudo eliminar la persona" + ex.Message;
                return response;
            }
        }
    }
}

