﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovilidadInteligenteUI.Models
{
    public class Unidad
    {
        [Display(Name = "ID")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string idUnidad { get; set; }

        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "Placa")]
        public string placa { get; set; }
        
        [Display(Name = "Estado")]
        public bool estado { get; set; }
    }
}
