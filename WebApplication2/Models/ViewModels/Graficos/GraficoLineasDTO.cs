using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels.Graficos
{
    public class GraficoLineasDTO
    {
        public string title { get; set; }
        public string subtitle { get; set; }
        public string yAxis { get; set; }
        public List<string> categories { get; set; }
        public List<SerieDTO> series { get; set; }
    }
}