using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Dominios;
using WebApplication2.Models.ViewModels;
using OfficeOpenXml;

namespace WebApplication2.Controllers
{
    public class PeliculaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var pelicula = new Repositorio<Pelicula>(db)
                            .TraerTodos().ToList()
                            .Select(p => new PeliculaGrillaViewModel
                            {
                                Nombre = p.Nombre,
                                Duracion = p.Duracion,
                                Id = p.Id,
                                Actores = p.Actores != null ? p.Actores
                                                .Select(c => c.Nombre).ToList() : new List<string>(),
                                Genero = p.Genero != null ? p.Genero.Nombre : "-",
                                FechaEstreno = p.FechaEstreno.HasValue ? p.FechaEstreno.Value.ToString("dd/MM/yyyy") : "-"
                            }).ToList();


            return View(pelicula);
        }

        public ActionResult GrillaSS()
        {
            var generos = new Repositorio<Genero>().TraerTodos().OrderBy(m => m.Nombre).Select(m => new SelectBoxITemViewModel
            {
                Id = m.Id,
                Texto = m.Nombre
            }).ToList();

            return View(generos);
        }

        public ActionResult GetData()
        {
            var start = Convert.ToInt32(Request.Form["start"]);
            var cantidad = Convert.ToInt32(Request.Form["length"]);
            var orderColumn = Request.Form["order[0][column]"];
            var orderDir = Request.Form["order[0][dir]"];
            var searchTerm = Request.Form["search[value]"];
            var nombre = Request.Form["nombre"];
            var genero = Request.Form["genero"];

            Guid genero_id = Guid.Empty;
            try { genero_id = new Guid(genero); } catch (Exception e) { }

            //TRAIGO TODOS
            var query = new Repositorio<Pelicula>(db).TraerTodos();
            int cantidadTotal = query.Count();

            //FILTRO
            query = query.Where(p =>
                    p.Nombre.Contains(nombre)
                    && (genero_id == Guid.Empty || p.GeneroId == genero_id)
                    );
            int cantidadFiltradas = query.Count();

            var columna = Convert.ToInt32(orderColumn);

            //ORDENO
            if (orderDir == "asc")
            {
                switch (columna)
                {
                    case 1:
                        query = query.OrderBy(p => p.Genero.Nombre);
                        break;
                    case 2:
                        query = query.OrderBy(p => p.Duracion);
                        break;
                    case 3:
                        query = query.OrderBy(p => p.FechaEstreno);
                        break;
                    default:
                        query = query.OrderBy(p => p.Nombre);
                        break;
                }
            }
            else
            {
                switch (columna)
                {
                    case 1:
                        query = query.OrderByDescending(p => p.Genero.Nombre);
                        break;
                    case 2:
                        query = query.OrderByDescending(p => p.Duracion);
                        break;
                    case 3:
                        query = query.OrderByDescending(p => p.FechaEstreno);
                        break;
                    default:
                        query = query.OrderByDescending(p => p.Nombre);
                        break;
                }
            }

            //TAKE SKIP + SELECT
            var data = query.Skip(start).Take(cantidad).ToList()
              .Select(p => new PeliculaGrillaViewModel
              {
                  Id = p.Id,
                  Nombre = p.Nombre,
                  Duracion = p.Duracion,
                  FechaEstreno = p.FechaEstreno.HasValue ? p.FechaEstreno.Value.ToString("dd/MM/yyyy") : "",
                  Genero = p.Genero != null ? p.Genero.Nombre : "-",
                  Actores = p.Actores != null ? p.Actores.OrderBy(n => n.Nombre).Select(n => n.Nombre).ToList() : new List<string>()
              }).ToList();


            //ARMO RESPUESTA
            var result = new { data = data, recordsTotal = cantidadTotal, recordsFiltered = cantidadFiltradas };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            var pelicula = new PeliculaABMViewModel();
            return View(pelicula);
        }

        [HttpPost]
        public ActionResult Create(PeliculaABMViewModel model)
        {
            ValidarPelicula(model);

            if (ModelState.IsValid)
            {
                new Repositorio<Pelicula>(db).Crear(model.ToEntity(db));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //for
        //new Repositorio<Pelicula>(db).Crear()

        public ActionResult Edit(Guid id)
        {
            var pelicula = new Repositorio<Pelicula>(db).Traer(id);
            var model = new PeliculaABMViewModel(pelicula);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PeliculaABMViewModel model)
        {
            #region EditValidations
            ValidarPelicula(model);
            #endregion

            if (ModelState.IsValid)
            {
                var repositorio = new Repositorio<Pelicula>(db);
                var pelicula = repositorio.Traer(model.Id);

                pelicula.Nombre = model.Nombre;
                pelicula.Duracion = model.Duracion;
                pelicula.FechaEstreno = model.FechaEstreno;
                pelicula.GeneroId = model.GeneroSeleccionado;

                pelicula.Actores.Clear();

                var repo_actores = new Repositorio<Actor>(db);
                foreach (var actor_id in model.ActoresSeleccionados)
                {
                    var actor = repo_actores.Traer(actor_id);
                    pelicula.Actores.Add(actor);
                }

                repositorio.Modificar(pelicula);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public void ValidarPelicula(PeliculaABMViewModel model)
        {
            if (model.FechaEstreno.HasValue && model.FechaEstreno.Value > DateTime.Today)
                ModelState.AddModelError("FechaEstreno", "La fecha no puede ser superior al dia de hoy");

            if (!model.GeneroSeleccionado.HasValue || model.GeneroSeleccionado.Value == Guid.Empty)
                ModelState.AddModelError("GeneroSeleccionado", "Debe seleccionar un genero");

        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            try
            {
                var repositorio = new Repositorio<Pelicula>(db);
                var pelicula = repositorio.Traer(id);

                if (pelicula == null)
                    return Json("Vehiculo inexistente", JsonRequestBehavior.AllowGet);

                repositorio.Eliminar(pelicula);

                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("ERROR", JsonRequestBehavior.AllowGet);
            }
        }
        public void CrearExcel()
        {
            using (var p = new ExcelPackage())
            {
                //A workbook must have at least on cell, so lets add one... 
                var ws = p.Workbook.Worksheets.Add("MySheet");

                //Pongo los títulos
                ws.Cells[1, 1].Value = "Película";
                ws.Cells[1, 1].Style.Font.Bold = true;
                ws.Cells[1, 2].Value = "Duración";
                ws.Cells[1, 2].Style.Font.Bold = true;
                ws.Cells[1, 3].Value = "Fecha de estreno";
                ws.Cells[1, 3].Style.Font.Bold = true;
                ws.Cells[1, 4].Value = "Género";
                ws.Cells[1, 4].Style.Font.Bold = true;


                var data = new Repositorio<Pelicula>(db).TraerTodos().ToList().Select(a => new PeliExcelViewModel
                {
                    Nombre = a.Nombre,
                    Duracion = a.Duracion,
                    Fecha = a.FechaEstreno.Value.ToString("dd/MM/yyyy"),
                    Genero = a.Genero != null ? a.Genero.Nombre : "-",

                }).ToList();

                ws.Cells["A2"].LoadFromCollection(data);

                //Save the new workbook. We haven't specified the filename so use the Save as method.
                p.SaveAs(new FileInfo(@"C:\Users\FCanella\Downloads\ListadoDePeliculas.xlsx"));

            }

        }

            //    public ActionResult CrearExcel()
            //{
            //    var pelicula = new Repositorio<Pelicula>(db)
            //                    .TraerTodos().ToList()
            //                    .Select(p => new PeliculaGrillaViewModel
            //                    {
            //                        Nombre = p.Nombre,
            //                        Duracion = p.Duracion,
            //                        Id = p.Id,
            //                        Actores = p.Actores != null ? p.Actores
            //                                        .Select(c => c.Nombre).ToList() : new List<string>(),
            //                        Genero = p.Genero != null ? p.Genero.Nombre : "-",
            //                        FechaEstreno = p.FechaEstreno.HasValue ? p.FechaEstreno.Value.ToString("dd/MM/yyyy") : "-"
            //                    }).ToList();


            //    //Creates a blank workbook. Use the using statment, so the package is disposed when we are done.
            //    using (var p = new ExcelPackage())
            //    {
            //        //A workbook must have at least on cell, so lets add one... 
            //        var ws = p.Workbook.Worksheets.Add("MySheet");
            //        //To set values in the spreadsheet use the Cells indexer.
            
            //        for(int i=1; i<=pelicula.Count();i++)
            //        {
            //            ws.Cells[1, i].Value = pelicula[i-1].Nombre;

            //        }

            //        //Save the new workbook. We haven't specified the filename so use the Save as method.
            //        p.SaveAs(new FileInfo(@"C:\Users\FCanella\Downloads\ListadoDePeliculas.xlsx"));


            //    }
            //    return RedirectToAction("Index");
            //}
    }
}
