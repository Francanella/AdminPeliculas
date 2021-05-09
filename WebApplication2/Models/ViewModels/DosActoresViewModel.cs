using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class DosActoresViewModel
    {
        public List<string> PelisActorUno { get; set; }
        public List<string> PelisActorDos { get; set; }
        public List<string> PelisActorUnoYDos { get; set; }
        public string NombreActorUno { get; set; }
        public string NombreActorDos { get; set; }
    }
}