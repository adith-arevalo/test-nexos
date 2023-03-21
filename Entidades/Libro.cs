using System.ComponentModel.DataAnnotations;

namespace ApiAutores.Entidades
{
    public class Libro
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "el campo ${0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        [Required(ErrorMessage = "el campo ${0} es requerido")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "el campo ${0} es requerido")]
        public int NumeroPaginas { get; set; }
        public List<AutorLibro> AutoresLibros { get; set; }
    }
}
