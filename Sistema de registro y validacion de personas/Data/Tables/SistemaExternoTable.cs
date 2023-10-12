using System.ComponentModel.DataAnnotations;

namespace Sistema_de_registro_y_validacion_de_personas.Data.Tables
{
    public class SistemaExternoTable
    {
        [Key, Required]
        public int id { get; set; }
        public string nombre { get; set; }
        public string token { get; set; }
    }
}
