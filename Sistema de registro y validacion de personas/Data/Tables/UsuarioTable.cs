using System.ComponentModel.DataAnnotations;

namespace Sistema_de_registro_y_validacion_de_personas.Data.Tables
{
    public class UsuarioTable
    {
        [Key, Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string contraseña { get; set; }
        public string rol { get; set; }
    }
}
