using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace SRVP.Data.Models
{
    public class Persona
    {
        [Key, Required]
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Required]
        public long cuil { get; set; }
        [Required]
        public long dni { get; set; }
        [Required]
        public DateOnly fechaNacimiento { get; set; }
        [Required]
        public string usuario { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string clave { get; set; }
        [Required]
        public string sal { get; set; }
        [Required]
        public string rol { get; set; }
        [Required]
        public DateTime alta { get; set; }
        [Required]
        public bool habilitado { get; set; } = false;
        [Required]
        public bool vivo { get; set; } = true;
        [Required]
        public bool estadoCrediticio { get; set; } = true;
    }
}
