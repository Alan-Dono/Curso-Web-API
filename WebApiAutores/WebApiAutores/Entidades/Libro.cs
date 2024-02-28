using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class Libro
    {
        public int id { get; set; }
        [Required]
        [MaxLength(120)]
        public string titulo { get; set; }
        public DateTime? fechaPublicacion { get; set; }
        public List<Comentario> comentarios { get; set; } // Propiedad de navegacion para acceder a todos los comentarios del libro
        public List<AutorLibro> AutoresLibros { get; set; } // Propiedad de navegacion para acceder a todos los autores del libro

    }
}
