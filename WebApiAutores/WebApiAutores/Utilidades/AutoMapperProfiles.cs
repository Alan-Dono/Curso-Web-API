using AutoMapper;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AutorCreacionDTO, Autor>(); // fuente / destino
            CreateMap<Autor, AutorLeerDTO>();
            CreateMap<Autor, AutorDTOConLibros>()
                .ForMember(autorDTO => autorDTO.libros, opciones => opciones.MapFrom(MapAutorLibros));
            CreateMap<AutorLeerDTO, Autor>(); // para el update

            CreateMap<Libro, LibroDTO>();
            CreateMap<Libro, LibroDTOConAutores>()
                .ForMember(libroDTO => libroDTO.autores, opciones => opciones.MapFrom(MapLibroDTOAutores));
            CreateMap<LibroPatchDTO, Libro>().ReverseMap();
            CreateMap<LibroCreacionDTO, Libro>()
                .ForMember(libro => libro.AutoresLibros, opciones => opciones.MapFrom(MapAutoreLibro));

            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();
        }

        private List<LibroDTO> MapAutorLibros(Autor autor, AutorLeerDTO autorDTO)
        {
            var resultado = new List<LibroDTO>();
            if (autor.AutoresLibros == null) { return resultado; }
            foreach (var autorLibro in autor.AutoresLibros)
            {
                resultado.Add(new LibroDTO()
                {
                    id = autorLibro.LibroId,
                    titulo = autorLibro.Libro.titulo
                });
            }
            return resultado;
        }
        private List<AutorLibro> MapAutoreLibro(LibroCreacionDTO libroCreacionDTO, Libro libro)
        {
            var resultado = new List<AutorLibro>();
            if (resultado == null) { return resultado;} // se valida en otra parte porque no es responsabilidad de esta clase
            foreach(var autorId in libroCreacionDTO.AutoresIds)
            {
                resultado.Add(new AutorLibro() { AutorId = autorId });
            }
            return resultado;
        }

        private List<AutorLeerDTO> MapLibroDTOAutores(Libro libro, LibroDTO libroDTO) 
        {
            var resultado = new List<AutorLeerDTO>();

            if (libro.AutoresLibros == null) { return resultado; }

            foreach(var autorLibro in libro.AutoresLibros)
            {
                resultado.Add(new AutorLeerDTO()
                {
                    id = autorLibro.AutorId,
                    nombre = autorLibro.Autor.nombre
                });
            }
            return resultado;
        }
    }
}
