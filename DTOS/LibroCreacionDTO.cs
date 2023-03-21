using ApiAutores.Entidades;
using System.ComponentModel.DataAnnotations;

namespace ApiAutores.DTOS
{
    public class LibroCreacionDTO
    {
       
        [StringLength(maximumLength: 250)]
        [Required(ErrorMessage = "el campo ${0} es requerido")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "el campo ${1} es requerido")]
        public DateTime FechaPublicacion { get; set; }
        [Required(ErrorMessage = "el campo ${2} es requerido")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "el campo ${3} es requerido")]
        public int NumeroPaginas { get; set; }

        public int AutorIdd { get; set; }
    }
}
