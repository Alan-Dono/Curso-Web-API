using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = CursoWebApi; Integrated Security = true",
                options => options.EnableRetryOnFailure());
        }

        public DbSet<Autor> Autores  { get; set; }
        public DbSet<Libro> Libros { get; set; }

    }
}
