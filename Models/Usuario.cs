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
        public string idUsuario { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido1 { get; set; }
        [Required]
        public string apellido2 { get; set; }
        [Required]
        public string correo { get; set; }
        [Required]
        public string clave { get; set; }
        [Required]
        public string telefono { get; set; }
        public int saldo { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string rol { get; set; }
        public bool estado { get; set; }
    }
}
