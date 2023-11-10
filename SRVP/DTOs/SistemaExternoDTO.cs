namespace SRVP.Data.DTOs
{
    public class SistemaExternoDTO
    {                    
        public string nombre { get; set; }
        public string secreto { get; set; }
        public string cuit { get; set; }
        public string paginaRetorno { get; set; }

        //necesitamos que ingrese la pagina de retorno en el POST?
        //lo necesitamos
    }
}
