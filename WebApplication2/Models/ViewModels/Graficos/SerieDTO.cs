using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels.Graficos
{
    public class SerieDTO
    {
        public string name { get; set; }
        public List<int> data { get; set; }
    }
}