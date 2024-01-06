using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class Autor
    {
        public int id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(120, ErrorMessage = "El campo {0} no debe tener mas de 120 caracteres")]
        public string nombre { get; set; }
        public List<AutoreLibro> AutoresLibros { get; set; }

    }
}
