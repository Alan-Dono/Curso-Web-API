namespace WebApiAutores.DTOs
{
    public class RespuestaAutenticacion
    {
        public string token { get; set; }
        public DateTime expiracion { get; set; }
    }
}
