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
            CreateMap<AutorLeerDTO, Autor>(); // para el update
            CreateMap<Libro, LibroDTO>();
            CreateMap<LibroCreacionDTO, Libro>();
        }
    }
}
