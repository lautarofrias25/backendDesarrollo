namespace Sistema_de_registro_y_validacion_de_personas.Data.Models
{
    public class Persona
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int cuil { get; set; }
        public int dni { get; set; }
        public string genero { get; set; }
        public int telefono { get; set; }
        public Boolean estado { get; set; }
        public Boolean estadoCrediticio { get; set; }
    }
}
