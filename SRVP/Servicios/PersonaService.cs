using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SRVP.Data.DTOs.Persona;
using SRVP.Data;
using System.Runtime.CompilerServices;
using SRVP.Data.Models;
using SRVP.Data.DTOs;
using SRVP.Interfaces;
using Mapster;
using System;

namespace SRVP.Servicios
{
    public class PersonaService : IPersonaService
    {
        private readonly SRVPContext _context;

        public PersonaService(SRVPContext context)
        {
            _context = context;
        }

        public async Task<Response<ICollection<PersonaDTO>>> GetPersonas()
        {
            var response = new Response<ICollection<PersonaDTO>>();
            response.Exito = false;
            response.Datos = null;
            try
            { 
                var personas = await _context.Personas.ToListAsync();
                if (personas != null)
                {
                    var personasDTO = new List<PersonaDTO>();
                    foreach (var persona in personas)
                    {
                        var personaDTO = persona.Adapt<PersonaDTO>();
                        /*personaDTO.id = persona.id;
                        personaDTO.nombre = persona.nombre;
                        personaDTO.apellido = persona.apellido;
                        personaDTO.cuil = persona.cuil;
                        personaDTO.dni = persona.dni;
                        personaDTO.fechaNacimiento = persona.fechaNacimiento; //VER
                        personaDTO.habilitado = persona.habilitado;
                        personaDTO.vivo = persona.vivo;
                        personaDTO.estadoCrediticio = persona.estadoCrediticio;*/
                        personasDTO.Add(personaDTO);
                    }
                    response.Datos = personasDTO;
                    response.Exito = true;
                    response.Mensaje = "Se recuperaron correctamente todas las personas";
                    return response;
                }
               
                response.Mensaje = "No se hallaron personas";
                return (response);
            }
            catch (Exception ex)
            {
                response.Mensaje = "Error interno : " + ex.Message;
                return (response);
            }
        }

        public async Task<Response<PersonaDTO>> GetPersona(int id)
        {
            var response = new Response<PersonaDTO>();
            response.Datos = null;
            response.Exito = false;
            try
            {
                var persona = await _context.Personas.FirstOrDefaultAsync(x => x.id == id);
                if (persona != null)
                {
                    var personaDTO = persona.Adapt<PersonaDTO>();
                    /*
                    {
                        id = persona.id,
                        nombre = persona.nombre,
                        apellido = persona.apellido,
                        cuil = persona.cuil,
                        dni = persona.dni,
                        fechaNacimiento = persona.fechaNacimiento,
                        habilitado = persona.habilitado,
                        vivo = persona.vivo,
                        estadoCrediticio = persona.estadoCrediticio
                    };
                    */
                    response.Datos = personaDTO;
                    response.Exito = true;
                    response.Mensaje = "Se recupero correctamente la persona";
                    return (response);
                };
                response.Mensaje = "No se hallo la persona";
                return (response);
            }
            catch (Exception ex)
            {
                response.Mensaje = "Error interno : " + ex.Message;
                return (response);
            }
        }

        public async Task<Response<bool>> GetEstadoCrediticio(int id) //el bool en response esta bien?
        {
            var response = new Response<bool>();
            response.Datos = false; //no se si esta bien
            response.Exito = false;
            try
            {
                var persona = await _context.Personas.FirstOrDefaultAsync(x => x.id == id);
                if (persona != null)
                {
                    
                    response.Datos = persona.estadoCrediticio;
                    response.Exito = true;
                    response.Mensaje = "Se recupero correctamente el estado crediticio";
                    return (response);
                };
                response.Mensaje = "No se hallo la persona";
                return (response);
            }
            catch (Exception ex)
            {
                response.Mensaje = "Error interno : " + ex.Message;
                return (response);
            }
        }

        public async Task<Response<Persona>> PostPersona(RegisterPersonaDTO personaDTO)
        {
            var response = new Response<Persona>();
            response.Datos = null;
            response.Exito = false;
            try
            {
                var personaDB = await _context.Personas.FirstOrDefaultAsync(x => x.cuil == personaDTO.cuil);
                if (personaDB == null)
                {
                    var persona = personaDTO.Adapt<Persona>();
                    /*
                    persona.nombre = personaDTO.nombre;
                    persona.apellido = personaDTO.apellido;
                    persona.cuil = personaDTO.cuil;
                    persona.dni = personaDTO.dni;
                    persona.fechaNacimiento = personaDTO.fechaNacimiento; //VER
                    persona.habilitado = personaDTO.habilitado;
                    persona.vivo = personaDTO.vivo;
                    persona.estadoCrediticio = personaDTO.estadoCrediticio;
                    */
                    var personaBD = await _context.Personas.AddAsync(persona);
                    await _context.SaveChangesAsync();
                    response.Exito = true;
                    response.Mensaje = "La persona se ha creado correctamente";
                    response.Datos = personaBD.Entity;
                    return (response);
                }
                response.Mensaje = "La persona que intenta crear ya existe";
                return (response);
            }
            catch (Exception ex)
            {
                response.Mensaje = "Error interno : " + ex.Message;
                return (response);
            }
        }

        public async Task<Response<Persona>> PutPersona(PersonaDTO personaDTO)
        {
            var response = new Response<Persona>();
            response.Datos = null;
            response.Exito = false;
            try
            {
                var personaBD = await _context.Personas.FindAsync(personaDTO.id);
                if (personaBD != null)
                {
                    personaBD = personaDTO.Adapt(personaBD); //no se si funciona hay que probar
                    /*personaBD.nombre = personaDTO.nombre;
                    personaBD.apellido = personaDTO.apellido;
                    personaBD.cuil = personaDTO.cuil;
                    personaBD.dni = personaDTO.dni;
                    personaBD.fechaNacimiento = personaDTO.fechaNacimiento; //VER
                    personaBD.habilitado = personaDTO.habilitado;
                    personaBD.vivo = personaDTO.vivo;
                    personaBD.estadoCrediticio = personaDTO.estadoCrediticio;*/
                    await _context.SaveChangesAsync();
                    response.Datos = personaBD;
                    response.Exito = true;
                    response.Mensaje = "La persona se ha modificado correctamente";
                    return response;
                }               
                response.Mensaje = "La persona a modificar no fue hallada";
                return (response);
            }
            catch (Exception ex)
            {
                response.Mensaje = "Error interno : " + ex.Message;
                return response;
            }
        }

        public async Task<Response<ICollection<PersonaDTO>>> PutEstadosCrediticios()
        {
            var response = new Response<ICollection<PersonaDTO>>();
            response.Exito = false;
            response.Datos = null;
            try
            {
                var personas = await _context.Personas.ToListAsync();
                if (personas != null)
                {
                    var personasDTO = new List<PersonaDTO>(); //no se si es necesario
                    foreach (var persona in personas)
                    {
                        var personaDTO = persona.Adapt<PersonaDTO>(); //no se si es necesario
                        var random = new Random();
                        persona.estadoCrediticio = (random.Next(2) == 1);
                        personasDTO.Add(personaDTO); //no se si es necesario
                    }
                    await _context.SaveChangesAsync();
                    response.Datos = personasDTO; //no se si es necesario
                    response.Exito = true;
                    response.Mensaje = "Se cambiaron todos los estados crediticios correctamente";
                    return response;
                }

                response.Mensaje = "No se hallaron personas";
                return (response);
            }
            catch (Exception ex)
            {
                response.Mensaje = "Error interno : " + ex.Message;
                return (response);
            }
        }

        public async Task<Response<Persona>> DeletePersona(int id)
        {
            var response = new Response<Persona>();
            response.Datos = null;
            response.Exito = false;
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
                response.Mensaje = "La persona a eliminar no fue hallada ";
                return (response);
            }
            catch (Exception ex)
            {
                response.Mensaje = "Error interno" + ex.Message;
                return response;
            }
        }
    }
}

