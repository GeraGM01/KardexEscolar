using System.ComponentModel.DataAnnotations;

namespace KardexEscolar.Models
{
    public class Materia_Profesor
    {
        //Modelo Combinado de Materia Profesor y alumno para obtener los datos de las materias y profesores que tiene un alumno

        public int Grupo { get; set; }

        [Display(Name = "Materia")]
        public string NombreMateria { get; set; }

        public string Nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        public string Apellido_Paterno { get; set; }

        [Display(Name = "Apellido Materno")]
        public string Apellido_Materno { get; set; }
    }
}
