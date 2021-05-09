using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Dominios;
using WebApplication2.Models.ViewModels;
using WebApplication2.Models.ViewModels.Graficos;


namespace WebApplication2.Controllers
{
    public class EstadisticasController : Controller
    {
        // GET: Estadisticas
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var RepoDePeliculas = new Repositorio<Pelicula>(db);
            var model = new EstadisticasViewModel();

            model.CantidadPeliculas = RepoDePeliculas.TraerTodos().Count();

            model.AgrupacionPorGenero = RepoDePeliculas
                                .TraerTodos().GroupBy(p => p.Genero.Nombre)
                                .Select(a => new AgrupacionPorGeneroViewModel
                                {
                                    NombreGenero = a.Max(p => p.Genero.Nombre),
                                    DuracionAvg = a.Average(p => p.Duracion),
                                    DuracionMin = a.Min(p => p.Duracion),
                                    DuracionMax = a.Max(p => p.Duracion),
                                    Cantidad = a.Count(),

                                }).ToList();

            model.PeliConMasActores = RepoDePeliculas
                                      .TraerTodos().OrderByDescending(p => p.Actores.Count())
                                      .Take(1).ToList().Select(a => new PeliConMasActoresViewModel
                                      {
                                          Nombre = a.Nombre,
                                          Genero = a.Genero.Nombre,
                                          Duracion = a.Duracion,
                                          FechaEstreno = a.FechaEstreno.HasValue ? a.FechaEstreno.Value.ToString("dd/MM/yyyy") : "-",
                                          Actores = a.Actores != null ? a.Actores.Select(p => p.Nombre).ToList() : new List<string>(),
                                          urlImage = a.URLImage,
                                          Sinopsis = a.Sinopsis,
                                          Id = a.Id,

                                      }).First();


            var pelisConMisActores = RepoDePeliculas
                                      .TraerTodos()
                                      .Where(p => p.Actores
                                      .Any(a => a.Nombre.Contains("Ricardo Darín")
                                      || a.Nombre.Contains("Leonardo Sbaraglia"))).ToList();

            model.DosActores = new DosActoresViewModel();

            model.DosActores.PelisActorUno = pelisConMisActores
                                           .Where(p =>
                                           p.Actores.Any(a => a.Nombre.Contains("Ricardo Darín")))
                                           .Select(p => p.Nombre).ToList();

            model.DosActores.NombreActorUno = "Ricardo Darín";

            model.DosActores.PelisActorDos = pelisConMisActores
                                        .Where(p =>
                                        p.Actores.Any(a => a.Nombre.Contains("Leonardo Sbaraglia")))
                                        .Select(p => p.Nombre).ToList();

            model.DosActores.NombreActorDos = "Leonardo Sbaraglia";

            model.DosActores.PelisActorUnoYDos = pelisConMisActores
                                        .Where(p =>
                                        p.Actores.Any(a => a.Nombre.Contains("Ricardo Darín"))
                                        || p.Actores.Any(a => a.Nombre.Contains("Leonardo Sbaraglia"))
                                        ).Select(p => p.Nombre).ToList();

            return View(model);
        }


        public ActionResult GetGraficoEstrenoPeliculas()
        {
            var repo = new Repositorio<Pelicula>(db);

            var model = new GraficoLineasDTO();

            //base data
            model.title = "Peliculas Estrenadas por año";

            //traigo data
            var minAnio = DateTime.Today.Year - 10;
            var maxAnio = DateTime.Today.Year;
            var data = repo.TraerTodos().Where(v => v.FechaEstreno.Value.Year <= maxAnio && v.FechaEstreno.Value.Year >= minAnio)
                .GroupBy(v => v.FechaEstreno.Value.Year)
                .Select(v => new
                {
                    Cantidad = v.Count(),
                    Anio = v.Key
                }).OrderBy(a => a.Anio).ToList();

            //series
            model.series = new List<SerieDTO>();

            var serie = new SerieDTO();
            serie.name = "Peliculas estrenadas";
            serie.data = data.Select(a => a.Cantidad).ToList();

            model.series.Add(serie);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGraficosEstrenosGenerosPorAño()
        {
            var repo = new Repositorio<Pelicula>(db);
            var model = new GraficoTortaDTO();

            //base data
            model.title = "Porcentaje de Peliculas por Genero";

            //traigo data
            var CantidadPelis = repo.TraerTodos().Count();

            var data = repo.TraerTodos().GroupBy(p => p.GeneroId).Select(a => new
            {
                nombreGenero = a.Max(p => p.Genero.Nombre),
                cantidadPelisPorGenero = a.Count(),
            }).ToList();

            //series
            model.series = new List<SerieTortaDTO>();

            var CantidadGeneros = data.Count();
            var nombre = data.Select(a => a.nombreGenero).ToList();

            var cantidadPelisPorGenero = data.Select(a => a.cantidadPelisPorGenero).ToList();

            for (int i = 0; i < CantidadGeneros; i++)
            {
                var serie = new SerieTortaDTO();
                serie.name = nombre[i];
                var porcentajeGenero = cantidadPelisPorGenero[i] * 100 / CantidadPelis;
                serie.y = porcentajeGenero;

                model.series.Add(serie);
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
       

    }
}

        