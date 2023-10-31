

namespace SRVP.Data.DTOs
{
    public class Response<T>
    {
        public T? Datos { get; set; }
        public bool Exito { get; set; } = false;
        public string Mensaje { get; set; } = "";
    }
}    