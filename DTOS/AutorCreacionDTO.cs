using System.ComponentModel.DataAnnotations;

namespace ApiAutores.DTOS
{
    public class AutorCreacionDTO
    {
        
        [Required(ErrorMessage = "el campo ${0} es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "el campo ${1} es requerido")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "el campo ${2} es requerido")]
        public string City { get; set; }
        [Required(ErrorMessage = "el campo ${3} es requerido")]
        [EmailAddress]
        public string Email { get; set; }
    }
}

