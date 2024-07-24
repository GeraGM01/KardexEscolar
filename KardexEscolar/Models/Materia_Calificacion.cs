namespace KardexEscolar.Models
{
    public class Materia_Calificacion
    {
        //Modelo combinado de Materia - Calificacion para poder guardar los datos de una materia y la
        //califificacion de esa materia de un alumno. Este modelo es auxilar a nivel de back, no esta 
        //definido como tabla en la base de datos.

        public int Clave { get; set; }
        public int Grupo { get; set; }
        public string NombreMateria { get; set; }
        public float Parcial_1 { get; set; }
        public float Parcial_2 { get; set; }
        public float Parcial_3 { get; set; }
        public float Parcial_4 { get; set; }
        public float Ordinario { get; set; }
        public float Extraordinario { get; set; }
        public float Titulo { get; set; }

    }
}
