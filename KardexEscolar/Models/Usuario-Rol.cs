using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KardexEscolar.Models
{
    public class Usuario_Rol
    {
        [Required]
        public int Id_Usuario { get; set; }

        [Required]
        public int Id_Rol { get; set; }

    }
}
