using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovilidadInteligenteUI.Models
{
    public class Linea
    {
        public string idLinea { get; set; }
        public string descripcion { get; set; }
        public int monto { get; set; }
        public int codigoCTP { get; set; }
        public bool estado { get; set; }
    }
}
