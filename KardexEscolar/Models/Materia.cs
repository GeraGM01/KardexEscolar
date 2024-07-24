using System.ComponentModel.DataAnnotations;

namespace KardexEscolar.Models
{
    public class Materia
    {
        [Key]
        public int Id_Materia { get; set; }
        [Required]
        [Display(Name = "Materia")]
        public string NombreMateria { get; set; }
        [Required]
        public int Clave { get; set; }
        [Required]
        public int Grupo { get; set; }
    }
}
