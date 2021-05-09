using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class EstadisticasViewModel
    {
        public int CantidadPeliculas { get; set; }
        public List<AgrupacionPorGeneroViewModel> AgrupacionPorGenero { get; set; }
        public PeliConMasActoresViewModel PeliConMasActores { get; set; }
        public DosActoresViewModel DosActores { get; set; }
    }
}