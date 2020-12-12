using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovilidadInteligenteUI.Models
{
    public class Bitacora
    {
        [Display(Name = "ID")]
        public string idBitacora { get; set; }

        [Display(Name = "Acción")]
        public string accion { get; set; }

        [Display(Name = "Error")]
        public string error { get; set; }

        [Display(Name = "Fecha")]
        public DateTime fecha { get; set; }

        [Display(Name = "ID Usuario")]
        public string idUsuario { get; set; }

        [Display(Name = "Estado")]
        public bool estado { get; set; }
    }
}
