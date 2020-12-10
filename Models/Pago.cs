using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovilidadInteligenteUI.Models
{
    public class Pago
    {

        public string idPago { get; set; }

        [Display(Name = "ID Usuario")]
        public string idUsuario { get; set; }

        [Display(Name = "ID Unidad")]
        public string idUnidad { get; set; }

        [Display(Name = "Monto")]
        public int monto { get; set; }

        [Display(Name = "Fecha de Pago")]
        public DateTime fechaPago { get; set; }

        [Display(Name = "Estado")]
        public bool estado { get; set; }
    }
}
