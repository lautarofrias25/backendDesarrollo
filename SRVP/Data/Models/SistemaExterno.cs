using System.ComponentModel.DataAnnotations;

namespace SRVP.Data.Models
{
    public class SistemaExterno
    {
        [Key, Required]
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
    }
}
