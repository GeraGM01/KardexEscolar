using System.ComponentModel.DataAnnotations;

namespace KardexEscolar.Models
{
    public class Alumno_Materia
    {
        [Required]
        public int Id_Alumno { get; set; }

        [Required]
        public int Id_Materia { get; set; }
    }
}
