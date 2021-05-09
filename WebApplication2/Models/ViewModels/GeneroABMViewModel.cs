using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models.Dominios;

namespace WebApplication2.Models.ViewModels
{
    public class GeneroABMViewModel : EntidadViewModel
    {
        public string Nombre { get; set; }
        public Guid? GeneroSeleccionado { get; set; }

        public GeneroABMViewModel()
        {

        }

        public GeneroABMViewModel(Genero model)
        {
            GeneroSeleccionado = model.GeneroId != null ? model.GeneroId.Value : Guid.Empty; 
            Nombre = model.Nombre;
            Id = model.Id;
        }

        public Genero ToEntity()
        {
            var genero = new Genero();
            genero.Nombre = Nombre;
            genero.GeneroId = GeneroSeleccionado != null ? GeneroSeleccionado.Value : Guid.Empty;

            return genero;
        }
    }

}
