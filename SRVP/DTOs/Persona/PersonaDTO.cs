﻿namespace SRVP.Data.DTOs.Persona
{
    public class PersonaDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public long cuil { get; set; }
        public long dni { get; set; }
        public DateOnly fechaNacimiento { get; set; }
        public string rol { get; set; }
        public bool habilitado { get; set; } = false;
        public bool vivo { get; set; }
        public bool estadoCrediticio { get; set; }
    }
}
