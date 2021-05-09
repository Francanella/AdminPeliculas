using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models.Dominios;

namespace WebApplication2.Models.ViewModels
{
    public class PeliculaGrillaViewModel : EntidadViewModel
    {
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public string FechaEstreno { get; set; }
        public string Genero { get; set; }
        public List<string> Actores { get; set; }
        public string urlImage { get; set; }
        public string Sinopsis { get; set; }


        public PeliculaGrillaViewModel()
        {
        }

        public PeliculaGrillaViewModel(Pelicula pelicula)
        {
            Nombre = pelicula.Nombre;
            Id = pelicula.Id;
            Duracion = pelicula.Duracion;
            Genero = pelicula.Genero.Nombre;
            FechaEstreno = pelicula.FechaEstreno.HasValue ? pelicula.FechaEstreno.Value.ToString("dd/MM/yyyy") : "-";
            Actores = pelicula.Actores.Select(a => a.Nombre).ToList();
            urlImage = pelicula.URLImage != null ? pelicula.URLImage : "data:image/png;base64,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";
            Sinopsis = pelicula.Sinopsis;
        }

    }
}