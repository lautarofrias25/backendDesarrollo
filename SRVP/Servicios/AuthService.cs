using Microsoft.AspNetCore.Mvc;
using SRVP.Data.DTOs.Persona;
using SRVP.Data.DTOs;
using SRVP.Data.Models;
using SRVP.Data;
using SRVP.DTOs.Persona;
using SRVP.DTOs;
using SRVP.Helpers;
using SRVP.Models;
using System.Text;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Mapster;
using SRVP.Interfaces;

namespace SRVP.Servicios
{
    public class AuthService : IAuthService
    {
        private readonly SRVPContext _context;
        private readonly IHasher _hasher;
        private readonly IJWT _jwt;
        public AuthService(SRVPContext context, IHasher hasher, IJWT jwt)
        {
            _context = context;
            _hasher = hasher;
            _jwt = jwt;
        }
        public async Task<Response<string>> loguearAccessCode(PersonaLoginDto user)
        {
            var response = new Response<string>();
            try
            {
                response.Datos = null;
                response.Exito = false;
                string passw = user.clave;
                var userBD = await _context.Personas.FirstOrDefaultAsync(x => x.usuario == user.nombre);
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
                            return (response);
                        }
                        response.Mensaje = "El sistema externo no esta registrado";
                        return (response);
                    }
                    response.Mensaje = "Las credenciales no son correctas";
                    return (response);
                }
                response.Mensaje = "El usuario no existe";
                return (response);
            }
            catch (Exception ex)
            {
                response.Mensaje = "Error interno: " + ex.Message;
                return (response);
            }
        }


        public async Task<Response<Persona>> registrarPersona(RegisterPersonaDTO persona)
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
                    userToSave.alta = DateTime.Now.ToUniversalTime();
                    
                    userToSave.rol = "Usuario";
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
                    return (response);
                }
                response.Mensaje = "El usuario o mail ya existen";
                return (response);
            }
            catch (Exception ex)
            {
                response.Mensaje = "Error interno: " + ex.Message;
                return (response);
            }
        }

        public async Task<Response<string>> loguearJWT(LoginJWTDto request)
        {
            var response = new Response<string>();
            response.Exito = false;
            response.Datos = null;
            try
            {
                if (await _context.SistemasExternos.AnyAsync(x => x.id.ToString() == request.clientId && x.secreto == request.clientSecret))
                {
                    var authorizationCodeBD = await _context.CodigosAccesos.FirstOrDefaultAsync(x => x.codigo == request.authorizationCode);
                    if (authorizationCodeBD != null && authorizationCodeBD.utilizado != true)
                    {
                        var personaBD = await _context.Personas.FindAsync(authorizationCodeBD.usuarioId);
                        var sistemaBD = await _context.SistemasExternos.FindAsync(authorizationCodeBD.sistemaExternoId);
                        XmlDocument doc = new XmlDocument();
                        doc.Load("ClavePrivada.xml");
                        string contenidoXML = doc.InnerXml;
                        var token = Asimetria.GenerarTokenJWT(contenidoXML, personaBD.nombre, personaBD.apellido, personaBD.cuil, personaBD.email, personaBD.estadoCrediticio, personaBD.rol, "SRVP", sistemaBD.nombre, DateTime.Now.AddDays(1));
                        response.Exito = true;
                        response.Datos = token;
                        response.Mensaje = "El token fue generado correctamente";
                        authorizationCodeBD.utilizado = true;
                        await _context.SaveChangesAsync();
                        return (response);
                    }
                    response.Mensaje = "El codigo de autorizacion no existe o ya fue utilizado";
                    return (response);
                }
                response.Mensaje = "El id o el secreto del cliente son incorrectos";
                return (response);
            }
            catch (Exception ex)
            {
                response.Mensaje = "Error interno: " + ex.Message;
                return (response);
            }
        }
        public async Task<Response<RespuestaLogin>> loguearInterno(LoginInternoDto user)
        {
            var respuesta = new Response<RespuestaLogin>();
            respuesta.Datos = null;
            respuesta.Exito = false;
            var userDB = await _context.Personas.FirstOrDefaultAsync(x => x.usuario == user.usuario);
            if (userDB != null)
            {
                try
                {
                    byte[] saltBytes = Convert.FromBase64String(userDB.sal);
                    byte[] passwBytes = Encoding.UTF8.GetBytes(user.clave);
                    byte[] combinedBytes = new byte[passwBytes.Length + saltBytes.Length];
                    Array.Copy(passwBytes, combinedBytes, passwBytes.Length);
                    Array.Copy(saltBytes, 0, combinedBytes, passwBytes.Length, saltBytes.Length);
                    string compHashString = _hasher.generateHash(combinedBytes);
                    if (userDB.clave == compHashString) //comparo con el string hasheado que tengo en la base de datos
                    {
                        var data = new RespuestaLogin
                        {
                            token = _jwt.GenerateToken(userDB),
                            rol = userDB.rol
                        };
                        respuesta.Datos = data;
                        respuesta.Exito = true;
                        respuesta.Mensaje = "El usuario se ha logueado correctamente";
                        return (respuesta);
                    }
                    respuesta.Mensaje = "Las credenciales no coinciden";
                    return (respuesta);
                }
                catch (Exception e)
                {
                    respuesta.Mensaje = "Error interno: " + e.Message;
                    return (respuesta);
                }
            }
            respuesta.Mensaje = "El usuario no existe";
            return (respuesta);
        }
    }
}
