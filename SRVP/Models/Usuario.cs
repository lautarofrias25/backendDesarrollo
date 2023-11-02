using System.ComponentModel.DataAnnotations;

namespace SRVP.Data.Models
{
    public class Usuario
    {
        [Key, Required]
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string clave { get; set; }
        [Required]
        public string rol { get; set; }
        [Required]
        public DateOnly alta { get; set; }
        [Required]
        public string sal { get; set; }

    }
}
