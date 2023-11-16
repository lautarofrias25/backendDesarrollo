using System.ComponentModel.DataAnnotations;

namespace SRVP.Data.DTOs.Persona
{
    public class RegisterPersonaDTO
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public long cuil { get; set; }
        public long dni { get; set; }
        public DateOnly fechaNacimiento { get; set; }
        public string usuario { get; set; }
        public string email { get; set; }
        public string clave { get; set; }
        //public string genero { get; set; }
        //public string rol {  get; set; }
        //public int telefono { get; set; }
        //public bool habilitado { get; set; } = false;
        //public bool vivo { get; set; }
        //public bool estadoCrediticio { get; set; }
    }
}
