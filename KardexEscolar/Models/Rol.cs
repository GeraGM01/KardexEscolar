using System.ComponentModel.DataAnnotations;

namespace KardexEscolar.Models
{
    public class Rol
    {
        [Required]
        public int Id_Rol {  get; set; }

        [Required]
        public string Nombre_Rol { get; set; }
    }
}
