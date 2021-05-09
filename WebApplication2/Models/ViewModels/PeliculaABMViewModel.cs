using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication2.Models.Dominios;

namespace WebApplication2.Models.ViewModels
{
    public class PeliculaABMViewModel : EntidadViewModel
    {
        public List<SelectBoxITemViewModel> ActoresDisponibles { get; set; }
        public List<Guid> ActoresSeleccionados { get; set; }

        public PeliculaABMViewModel()
        {
            GenerosDisponibles = new List<SelectBoxITemViewModel>();
            ActoresDisponibles = new List<SelectBoxITemViewModel>();
            ActoresSeleccionados = new List<Guid>();
            LlenarListas();
        }

        public PeliculaABMViewModel(Pelicula model)
        {
            Id = model.Id;
            Nombre = model.Nombre;
            FechaEstreno = model.FechaEstreno;
            Duracion = model.Duracion;
            GeneroSeleccionado = model.GeneroId != null ? model.GeneroId.Value : Guid.Empty;
            ActoresSeleccionados = model.Actores != null ? model.Actores.Select(c => c.Id).ToList() : new List<Guid>();
            LlenarListas();
        }

        [Required(ErrorMessage = "Por favor complete el nombre")]
        public string Nombre { get; set; }
        [Range(30, 200, ErrorMessage = "Complete la duracion")]
        public int Duracion { get; set; }
        [Required(ErrorMessage = "Por favor complete la fecha de estreno")]
        public DateTime? FechaEstreno { get; set; }

        public List<SelectBoxITemViewModel> GenerosDisponibles { get; set; }
        public Guid? GeneroSeleccionado { get; set; }

        public void LlenarListas()
        {
            GenerosDisponibles = new Repositorio<Genero>().TraerTodos().OrderBy(g => g.Nombre).Select(g => new SelectBoxITemViewModel
            {
                Id = g.Id,
                Texto = g.Nombre
            }).ToList();

            ActoresDisponibles = new Repositorio<Actor>().TraerTodos().OrderBy(m => m.Nombre).Select(m => new SelectBoxITemViewModel
            {
                Id = m.Id,
                Texto = m.Nombre
            }).ToList();
        }

        public Pelicula ToEntity(ApplicationDbContext db)
        {
            var pelicula = new Pelicula();
            pelicula.Nombre = Nombre;
            pelicula.Duracion = Duracion;
            pelicula.FechaEstreno = FechaEstreno;
            pelicula.GeneroId = GeneroSeleccionado.Value;

            pelicula.Actores = new List<Actor>();
            var repo_actores = new Repositorio<Actor>(db);

            foreach (var actor_id in ActoresSeleccionados)
            {
                var actor = repo_actores.Traer(actor_id);
                pelicula.Actores.Add(actor);
            }

            return pelicula;
        }
    }
}