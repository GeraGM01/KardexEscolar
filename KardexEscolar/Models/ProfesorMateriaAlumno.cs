namespace KardexEscolar.Models
{
    public class ProfesorMateriaAlumno
    {
        //Modelo que me va a ayudar a encapsular los datos de calificaciones y materias que tiene un alumno

        public List<Materia_Calificacion> KardexCalificacion { get; set; }

        public List<Materia_Profesor> KardexMateriaProfesor { get; set; }

    }
}
