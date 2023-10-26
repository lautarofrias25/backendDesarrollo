using System.ComponentModel.DataAnnotations;

namespace SRVP.Data.DTOs
{
    public class Usuario
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public byte[] clave { get; set; }
        [Required]
        public string rol { get; set; }
    }
}
