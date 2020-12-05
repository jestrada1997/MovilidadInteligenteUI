using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovilidadInteligenteUI.Models
{
    public class Bitacora
    {
        public string idBitacora { get; set; }
        public string accion { get; set; }
        public string error { get; set; }
        public DateTime fecha { get; set; }
        public string idUsuario { get; set; }
        public bool estado { get; set; }
    }
}
