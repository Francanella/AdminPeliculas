using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models.Dominios;

namespace WebApplication2.Models.ViewModels
{
    public class AgrupacionPorGeneroViewModel
    {
        public int Cantidad { get; set; }
        public double DuracionAvg { get; set; }
        public int DuracionMax { get; set; }
        public int DuracionMin { get; set; }
        public string NombreGenero { get; set; }
    }
}