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
    public class GeneroController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var genero = new Repositorio<Genero>(db)
                            .TraerTodos().ToList()
                            .Select(g => new GeneroGrillaViewModel
                            {
                                Id = g.Id,
                                Nombre = g.Nombre

                            }).ToList();
            return View(genero);
        }

        public ActionResult Create()
        {
            var genero = new GeneroABMViewModel();

            return View(genero);
        }

        [HttpPost]
        public ActionResult Create(GeneroABMViewModel model)
        {
            if (ModelState.IsValid)
            {
                new Repositorio<Genero>(db).Crear(model.ToEntity());
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            var genero = new Repositorio<Genero>(db).Traer(id);
            var model = new GeneroABMViewModel(genero);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(GeneroABMViewModel model)
        {

            if (ModelState.IsValid)
            {
                var repositorio = new Repositorio<Genero>(db);
                var genero = repositorio.Traer(model.Id);

                genero.Nombre = model.Nombre;
                genero.GeneroId = model.GeneroSeleccionado;

                repositorio.Modificar(genero);

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}