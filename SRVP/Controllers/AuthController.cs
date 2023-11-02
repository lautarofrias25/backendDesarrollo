using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SRVP.Data;
using SRVP.Data.DTOs;
using SRVP.Models;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SRVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SRVPContext _context;
        public AuthController (SRVPContext context)
        {
            _context = context;
        }
        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthController>
        [HttpPost]
        public async Task<ActionResult<Response<string>>> loguearAccesCode([FromBody] UsuarioLoginDto user)
        {
            var response = new Response<string>();
            try
            {
                response.Datos = null;
                response.Exito = false;
                string passw = user.clave;
                var userBD = await _context.Usuarios.FirstOrDefaultAsync(x => x.nombre == user.nombre);
                if (userBD != null)
                {
                    byte[] saltBytes = Convert.FromBase64String(userBD.sal);
                    byte[] passwBytes = Encoding.UTF8.GetBytes(passw);
                    byte[] combinedBytes = new byte[passwBytes.Length + saltBytes.Length];
                    Array.Copy(passwBytes, combinedBytes, passwBytes.Length);   //comentar para entender mejor despues
                    Array.Copy(saltBytes, 0, combinedBytes, passwBytes.Length, saltBytes.Length);
                    SHA256 sha256 = SHA256.Create();
                    byte[] compHash = sha256.ComputeHash(combinedBytes);
                    string compHashString = Convert.ToBase64String(compHash);
                    if (userBD.clave == compHashString)
                    {

                        var sistemaExterno = await _context.SistemasExternos.FirstOrDefaultAsync(x => x.nombre == user.sistema);
                        if (sistemaExterno != null)
                        {
                            var accesCode = new CodigoAcceso();
                            // accesCode.codigo = generateCode() TO DO FUNCTION }
                            accesCode.creacion = DateOnly.FromDateTime(DateTime.Now);
                            accesCode.sistemaExternoId = sistemaExterno.id;
                            accesCode.usuarioId = userBD.id;
                            await _context.CodigosAccesos.AddAsync(accesCode);
                            await _context.SaveChangesAsync();
                            response.Datos = accesCode.codigo;
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
                return BadRequest(response);
            }
        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
