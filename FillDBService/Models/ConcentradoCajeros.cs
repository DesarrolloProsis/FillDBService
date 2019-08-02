using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillDBService.Models
{
    class ConcentradoCajeros
    {
        public List<PropertiesCajeros> AllCajeros = new List<PropertiesCajeros>();
    }
    class PropertiesCajeros
    {
        public string Numero_Capufe { get; set; }
        public string Numero_Gea { get; set; }
    }
}
