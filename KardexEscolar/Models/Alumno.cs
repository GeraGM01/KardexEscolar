using System.ComponentModel.DataAnnotations;

namespace KardexEscolar.Models
{
    public class Alumno
    {
        [Key]
        public int Id_Alumno { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido_Paterno { get; set; }
        [Required]
        public string Apellido_Materno { get; set; }
        [Required]
        public int Semestre { get; set; }
        [Required]
        public int Clave_Unica { get; set; }
        public int Id_Usuario { get; set; }
    }
}
