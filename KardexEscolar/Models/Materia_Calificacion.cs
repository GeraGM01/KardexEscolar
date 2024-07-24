using System.ComponentModel.DataAnnotations;

namespace KardexEscolar.Models
{
    public class Materia_Calificacion
    {
        //Modelo combinado de Materia - Calificacion para poder guardar los datos de una materia y la
        //califificacion de esa materia de un alumno. Este modelo es auxilar a nivel de back, no esta 
        //definido como tabla en la base de datos.

        public int Clave { get; set; }
        public int Grupo { get; set; }
        [Display(Name = "Materia")]
        public string NombreMateria { get; set; }
        [Display(Name = "P1")]
        public float Parcial_1 { get; set; }
        [Display(Name = "P2")]
        public float Parcial_2 { get; set; }
        [Display(Name = "P3")]
        public float Parcial_3 { get; set; }
        [Display(Name = "P4")]
        public float Parcial_4 { get; set; }
        [Display(Name = "EO")]
        public float Ordinario { get; set; }
        [Display(Name = "EE")]
        public float Extraordinario { get; set; }
        [Display(Name = "ET")]
        public float Titulo { get; set; }

    }
}
