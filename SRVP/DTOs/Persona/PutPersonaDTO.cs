namespace SRVP.DTOs.Persona
{
    public class PutPersonaDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public long cuil { get; set; }
        public long dni { get; set; }
        public DateOnly fechaNacimiento { get; set; }
        public string email { get; set; }
        public bool habilitado { get; set; } = false;
        public bool vivo { get; set; }
        public bool estadoCrediticio { get; set; }
    }
}
