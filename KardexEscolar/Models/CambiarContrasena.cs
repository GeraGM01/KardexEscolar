using System.ComponentModel.DataAnnotations;

namespace KardexEscolar.Models
{
    public class CambiarContrasena
    {
        [Required]
        public string ContrasenaActual { get; set; }

        [Required]
        public string ContrasenaNueva { get; set; }

        [Required]
        [Compare("ContrasenaNueva", ErrorMessage = "La nueva contraseña y la confirmación deben coincidir.")]
        public string ConfirmarContrasenaNueva { get; set; }

    }
}
