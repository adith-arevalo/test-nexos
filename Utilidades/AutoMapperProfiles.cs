using ApiAutores.DTOS;
using ApiAutores.Entidades;
using AutoMapper;

namespace ApiAutores.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AutorCreacionDTO, Autor>().ReverseMap();

            CreateMap<Autor, AutorDTO>().ReverseMap();

            CreateMap<Autor, AutorDTOConLibros>()
                .ForMember(AutorDTO => AutorDTO.Libros, opciones => opciones.MapFrom(MapAutorDTOLibros));

            CreateMap<LibroCreacionDTO, Libro>()
                .ForMember(libro => libro.AutoresLibros, opciones => opciones.MapFrom(MapAutoresLibros)).ReverseMap();

            CreateMap<Libro, LibroDTO>().ReverseMap();

            CreateMap<Libro, LibroDTOConAutores>()
                .ForMember(LibroDTO => LibroDTO.Autores, opciones => opciones.MapFrom(MapLibroDTOAutores));




        }

        private List<LibroDTO> MapAutorDTOLibros(Autor autor, AutorDTO autorDTO)
        {

            var result = new List<LibroDTO>();

            if (autor.AutoresLibros == null) { return result; }

            foreach (var autorLibro in autor.AutoresLibros)
            {
                result.Add(new LibroDTO()
                {
                    Id = autorLibro.LibroId,
                    Titulo = autorLibro.Libro.Titulo,
                });
            }

            return result;
        }
        private List<AutorDTO> MapLibroDTOAutores(Libro libro, LibroDTO libroDTO)
        {
            var result = new List<AutorDTO>();

            if (libro.AutoresLibros == null) { return result; }

            foreach (var autorlibro in libro.AutoresLibros)
            {
                result.Add(new AutorDTO()
                {
                    Id = autorlibro.AutorId,
                    Name = autorlibro.Autor.Name,
                });
            }

            return result;


        }
        private List<AutorLibro> MapAutoresLibros(LibroCreacionDTO libroCreacionDTO, Libro libro)
        {
            var result = new List<AutorLibro>();

            if (libroCreacionDTO.AutorIdd == null)
            {
                return result;
            }

            //foreach (var autorId in libroCreacionDTO.AutorIdd)
            //{
            //    result.Add(new AutorLibro() { AutorId = autorId });
            //}

            return result;
        }


    }
}
