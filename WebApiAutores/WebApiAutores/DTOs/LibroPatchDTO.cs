using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.DTOs
{
    public class LibroPatchDTO
    {
        [StringLength(maximumLength: 120)]
        [Required]
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
