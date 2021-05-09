using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Dominios;
using WebApplication2.Models.ViewModels;

namespace WebApplication2.Controllers
{
    public class FichaController : Controller
    {
        // GET: Ficha

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var ficha = new Repositorio<Genero>(db)
                            .TraerTodos().ToList()
                            .Select(g => new SelectBoxITemViewModel
                            {
                                Id = g.Id,
                                Texto = g.Nombre

                            }).ToList();

            return View(ficha);
        }

        public ActionResult getPelisByGenero(Guid id)
        {
            var peliculas = new Repositorio<Pelicula>(db)
                            .TraerTodos().Where(p => p.GeneroId == id)
                            .Select(v => new SelectBoxITemViewModel
                            {
                                Id = v.Id,
                                Texto = v.Nombre

                            }).ToList();

            return Json(peliculas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getPeli(Guid id)
        {
            var pelicula = new Repositorio<Pelicula>(db).Traer(id);
            var model = new PeliculaGrillaViewModel(pelicula);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        
    }
}