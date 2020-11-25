using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovilidadInteligenteUI.Models
{
    public class Pago
    {
        public string idPago { get; set; }
        public string idUsuario { get; set; }
        public string idUnidad { get; set; }
        public int monto { get; set; }
        public DateTime fechaPago { get; set; }
        public bool estado { get; set; }
    }
}
