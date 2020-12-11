using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovilidadInteligenteUI.Models
{
    public class Deposito
    {
        [Display(Name = "ID")]
        public string idDeposito { get; set; }

        [Display(Name = "IDUsuario")]
        public string idUsuario { get; set; }

        [Display(Name = "Monto")]
        public int monto { get; set; }

        [Display(Name = "Fecha del Deposito")]
        public DateTime fechaDeposito { get; set; }

        [Display(Name = "Estado")]
        public bool estado { get; set; }
    }
}
