using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FillDBService.Models
{

    [NotMapped]
    public class EventoCarril
    {
        public DateTime Hora_Inicio { get; set; }
        public DateTime Hora_Fin { get; set; }
        public string Carril { get; set; }

    }
}