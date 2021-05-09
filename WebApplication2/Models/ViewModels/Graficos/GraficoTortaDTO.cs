using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels.Graficos
{
    public class GraficoTortaDTO
    {
        public string title { get; set; }
        public List<SerieTortaDTO> series { get; set; }
    }
}