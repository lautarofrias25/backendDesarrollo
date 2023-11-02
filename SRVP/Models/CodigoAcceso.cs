using SRVP.Data.Models;

namespace SRVP.Models
{
    public class CodigoAcceso
    {
        public Guid  id { get; set; }
        public Guid sistemaExternoId { get; set; }
        public int usuarioId { get; set; }
        public string codigo { get; set; } //access code
        public bool utilizado { get; set; } = false;
        public DateOnly creacion { get; set; }

    }
}
