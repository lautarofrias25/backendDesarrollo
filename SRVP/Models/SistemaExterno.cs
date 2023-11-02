using System.ComponentModel.DataAnnotations;

namespace SRVP.Data.Models
{
    public class SistemaExterno
    {
        [Key, Required]
        public Guid id { get; set; }
        [Required]
        public string nombre { get; set; }
        public string secreto { get; set; }
        public string paginaRetorno { get; set; }
        public string publicoId { get; set; }
    }
}
