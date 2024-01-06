using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.DTOs
{
    public class LibroCreacionDTO
    {
        [StringLength(maximumLength:120)]
        public string Titulo { get; set; }
        public List<int> AutoresIds { get; set; }
    }
}
