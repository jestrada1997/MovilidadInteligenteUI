using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovilidadInteligenteUI.Models
{
    public class Deposito
    {
        public string idDeposito { get; set; }
        public string idUsuario { get; set; }
        public int monto { get; set; }
        public DateTime fechaDeposito { get; set; }
        public bool estado { get; set; }
    }
}
