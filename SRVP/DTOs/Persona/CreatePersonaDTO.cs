namespace SRVP.Data.DTOs.Persona
{
    public class CreatePersonaDTO
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int cuil { get; set; }
        public int dni { get; set; }
        public DateOnly fechaNacimiento { get; set; }
        public string genero { get; set; }
        public int telefono { get; set; }
        public bool habilitado { get; set; } = false;
        public bool estado { get; set; }
        public bool estadoCrediticio { get; set; }
    }
}
