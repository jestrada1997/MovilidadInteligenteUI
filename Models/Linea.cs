using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovilidadInteligenteUI.Models
{
    public class Linea
    {
        [Display(Name = "ID")]
        public string idLinea { get; set; }

        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        [Display(Name = "Monto")]
        public int monto { get; set; }

        [Display(Name = "Código CTP")]
        public int codigoCTP { get; set; }

        [Display(Name = "Estado")]
        public bool estado { get; set; }
    }
}
