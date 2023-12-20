namespace WebApiAutores.Entidades
{
    public class Autor
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public List<Libro> libros { get; set; }
    }
}
