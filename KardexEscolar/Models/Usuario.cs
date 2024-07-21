﻿using System.ComponentModel.DataAnnotations;

namespace KardexEscolar.Models
{
    public class Usuario
    {
        [Key]
        public int Id_Usuario { get; set; }

        [Required(ErrorMessage = "El campo Clave Unica es obligatorio")]
        public int Clave_Unica { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        public string Contrasena { get; set; }
    }
}