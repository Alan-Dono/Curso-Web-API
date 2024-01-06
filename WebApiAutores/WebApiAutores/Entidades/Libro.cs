using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class Libro
    {
        public int id { get; set; }
        [Required]
        [MaxLength(120)]
        public string titulo { get; set; }
        public List<Comentario> comentarios { get; set; }


    }
}
