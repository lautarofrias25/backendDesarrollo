using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SRVP.Data;
using SRVP.Data.DTOs;
using SRVP.Data.DTOs.Persona;
using SRVP.Data.Models;
using SRVP.DTOs.Persona;
using SRVP.Helpers;
using SRVP.Models;
using System.Data.SqlTypes;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SRVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SRVPContext _context;
        private readonly Hasher _hasher;
        public AuthController (SRVPContext context, Hasher hasher)
        {
            _context = context;
            _hasher = hasher;
        }
        // POST api/<AuthController>
        [HttpPost]
        public async Task<ActionResult<Response<string>>> loguearAccessCode([FromBody] PersonaLoginDto user)
        {
            var response = new Response<string>();
            try
            {
                response.Datos = null;
                response.Exito = false;
                string passw = user.clave;
                var userBD = await _context.Personas.FirstOrDefaultAsync(x => x.nombre == user.nombre);
                if (userBD != null)
                {
                    byte[] saltBytes = Convert.FromBase64String(userBD.sal); //convierto de base64 a bytes
                    byte[] passwBytes = Encoding.UTF8.GetBytes(passw); //obtengo los bytes de la clave que viene del front
                    byte[] combinedBytes = new byte[passwBytes.Length + saltBytes.Length];
                    Array.Copy(passwBytes, combinedBytes, passwBytes.Length);   //copia los elementos de passwBytes en combinedBytes
                                                                                //arrancando desde el inicio y le especifico la longitud
                    Array.Copy(saltBytes, 0, combinedBytes, passwBytes.Length, saltBytes.Length);
                    //este copia los elementos de saltBytes arrancando desde el indice 0 de saltBytes, en combinedBytes a partir del indice
                    //que lo determino por passwBytes.Length y le especifico la longitud de lo que voy a copiar con saltBytes.Length
                    // asi me quedan concatenados los Bytes de passw y la salt
                    
                                                                                 //llamo al helper que hashee los bytes de combinedBytes
                    string compHashString = _hasher.generateHash(combinedBytes); // los convierta a base64
                    if (userBD.clave == compHashString) //comparo con el string hasheado que tengo en la base de datos
                    {
                        var sistemaExterno = await _context.SistemasExternos.FirstOrDefaultAsync(x => x.nombre == user.sistema);
                        if (sistemaExterno != null)
                        {
                            var accessCode = new CodigoAcceso();
                            accessCode.codigo = _hasher.generateAccessCode(); //llamo a la funcion del helper que me genera un accessCode
                            accessCode.creacion = DateOnly.FromDateTime(DateTime.Now);
                            accessCode.sistemaExternoId = sistemaExterno.id;
                            accessCode.usuarioId = userBD.id;
                            await _context.CodigosAccesos.AddAsync(accessCode);
                            await _context.SaveChangesAsync();
                            response.Datos = sistemaExterno.paginaRetorno + accessCode.codigo;
                            response.Exito = true;
                            response.Mensaje = "Credenciales correctas, codigo de acceso generado correctamente";
                            return Ok(response);
                        }
                        response.Mensaje = "El sistema externo no esta registrado";
                        return BadRequest(response);
                    }
                    response.Mensaje = "Las credenciales no son correctas";
                    return BadRequest(response);
                }
                response.Mensaje = "El usuario no existe";
                return BadRequest(response);
            }
            catch(Exception ex)
            {
                response.Mensaje = "Error interno: " + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        // PUT api/<AuthController>/5
        [HttpPost]
        public async Task<ActionResult<Response<Persona>>> registrarPersona([FromBody] RegisterPersonaDTO persona)
        {
            var response = new Response<Persona>()
            {
                Exito = false,
                Datos = null
            };
            try
            {
                if (!await _context.Personas.AnyAsync(x => x.usuario == persona.usuario || x.email == persona.email))
                {
                    var userToSave = persona.Adapt<Persona>();
                    userToSave.alta = DateTime.Now;
                    byte[] passwBytes = Encoding.UTF8.GetBytes(persona.clave);
                    byte[] salt = _hasher.generateSalt();
                    userToSave.sal = Convert.ToBase64String(salt);
                    byte[] combinedBytes = new byte[passwBytes.Length + salt.Length];
                    Array.Copy(passwBytes, combinedBytes, passwBytes.Length);   
                    Array.Copy(salt, 0, combinedBytes, passwBytes.Length, salt.Length);
                    userToSave.clave = _hasher.generateHash(combinedBytes);
                    var userBD = await _context.Personas.AddAsync(userToSave);
                    await _context.SaveChangesAsync();
                    response.Datos = userBD.Entity;
                    response.Exito = true;
                    response.Mensaje = "Registro completado con exito";
                    return StatusCode(StatusCodes.Status201Created, response); 
                }
                response.Mensaje = "El usuario o mail ya existen";
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Mensaje = "Error interno: " + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
