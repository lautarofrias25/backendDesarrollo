using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SRVP.Data;
using SRVP.Data.Models;
using SRVP.Data.DTOs;
using SRVP.Helpers;
/*
namespace SRVP.Controllers
{
    [Route("Usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly SRVPContext _context;
        private readonly Hasher _hasher;
        private readonly Jwt _jwt;

        public UsuarioController(SRVPContext context, Hasher hasher, Jwt jwt)
        {
            _context = context;
            _hasher = hasher;
            _jwt = jwt;
        }

        [HttpPost]
        public async Task<ActionResult<Response<UsuarioRegisterDTO>>> RegistrarUsuario(UsuarioRegisterDTO user)
        {
            var respuesta = new Response<UsuarioRegisterDTO>();
            if (await _context.Usuarios.FindAsync(user.nombre) == null)
            {
                try
                {
                    byte[] hash = _hasher.GenerateHash(user.clave);
                    Usuario usuario = new Usuario
                    {
                        nombre = user.nombre,
                        clave = hash,
                        rol = user.rol,
                    };
                    await _context.Usuarios.AddAsync(usuario);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = user;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Usuario creado correctamente";
                    return Ok(respuesta);

                }
                catch (Exception e)
                {
                    respuesta.Datos = user;
                    respuesta.Exito = false;
                    respuesta.Mensaje = "No se pudo registrar el usuario:" + e.Message;
                    return BadRequest(respuesta);
                }
                
            };
            respuesta.Datos = user;
            respuesta.Exito = false;
            respuesta.Mensaje = "No se pudo registrar el usuario porque ya existe el nombre";
            return BadRequest(respuesta);
        }

        [HttpPost]
        public async Task<ActionResult<Response<string>>> LoguearUsuario(UsuarioLoginDto user)
        {
            var respuesta = new Response<string>();
            var userDB = await _context.Usuarios.FindAsync(user.nombre);
            if (userDB.nombre == user.nombre && userDB.clave == _hasher.GenerateHash(user.clave))
            {
                try
                {
                    respuesta.Datos = _jwt.GenerateToken(userDB);
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El usuario se ha logueado correctamente";
                    return Ok(respuesta);
                }
                catch (Exception e)
                {
                    respuesta.Datos ="";
                    respuesta.Exito = false;
                    respuesta.Mensaje = "El usuario no se ha podido loguear: " + e.Message;
                    return BadRequest(respuesta);
                } 
            }
            respuesta.Datos ="";
            respuesta.Exito = false;
            respuesta.Mensaje = "Las credenciales no coinciden";
            return BadRequest(respuesta);
        }
    }
     
}*/
