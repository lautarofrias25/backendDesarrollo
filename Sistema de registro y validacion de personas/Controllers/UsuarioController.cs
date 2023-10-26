using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SRVP.Data;
using SRVP.Data.DTOs;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace SRVP.Controllers
{
    [Route("Usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly SRVPContext _context;

        public UsuarioController(SRVPContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> RegistrarUsuario(UsuarioDTO user)
        {
            if (await _context.Usuarios.FindAsync(user.nombre) == null)
            {
                SHA256 sha256hash = SHA256.Create();
                byte[] hash = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(user.clave));
                Usuario usuario = new Usuario
                {
                    nombre = user.nombre,
                    clave = hash,
                    rol = user.rol,
                };
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return Ok(usuario);
            };
            return BadRequest(user);
        }

        [HttpPost]
        public async 
    }
     
}
