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
        public int cuil { get; set; }
        [Required]
        public int dni { get; set; }
        [Required]
        public DateOnly fechaNacimiento { get; set; }
        [Required]
        public string genero { get; set; }
        [Required]
        public int telefono { get; set; } //ver
        [Required]
        public bool habilitado { get; set; } = false;
        [Required]
        public bool estado { get; set; } = true;
        [Required]
        public bool estadoCrediticio { get; set; } = true;
    }
}
