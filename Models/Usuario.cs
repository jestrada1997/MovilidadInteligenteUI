using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovilidadInteligenteUI.Models
{
    public class Usuario
    {
        [Display(Name = "ID")]
        public string idUsuario { get; set; }
 
        [Display(Name = "Nombre")]
        [Required]
        public string nombre { get; set; }

        [Display(Name = "Primer Apellido")]
        [Required]
        public string apellido1 { get; set; }
        [Display(Name = "Segundo Apellido")]
        [Required]
        public string apellido2 { get; set; }
        
        [Display(Name = "Correo")]
        [Required]
        public string correo { get; set; }

        [Display(Name = "Contraseña")]
        [Required]
        public string clave { get; set; }

        [Display(Name = "Teléfono")]
        [Required]
        public string telefono { get; set; }
        
        [Display(Name = "Saldo")]
        public int saldo { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime fechaCreacion { get; set; }

        [Display(Name = "Rol")]
        public string rol { get; set; }

        [Display(Name = "Estado")]
        public bool estado { get; set; }
    }
}
