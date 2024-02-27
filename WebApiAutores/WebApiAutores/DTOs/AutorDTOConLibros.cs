namespace WebApiAutores.DTOs
{
    public class AutorDTOConLibros: AutorLeerDTO
    {
        public List<LibroDTO> libros { get; set; }
    }
}
