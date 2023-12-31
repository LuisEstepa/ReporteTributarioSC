﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReporteTributario.Models.Entities
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key]
            public int IdUsuario { get; set; }

            public string NombreUsuario { get; set; }

            public string Correo { get; set; }

            public string Clave { get; set; }

            [NotMapped]
            public string ConfirmarClave { get; set; }
            public bool Restablecer { get; set; }
            public bool Confirmado { get; set; }
            public string Token { get; set; }
        
    }
}
