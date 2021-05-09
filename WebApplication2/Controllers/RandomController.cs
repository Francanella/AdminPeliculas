using InduccionV7.Website.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Helpers;
using WebApplication2.Models;
using WebApplication2.Models.Dominios;


namespace WebApplication2.Controllers
{
    public class RandomController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Random
        public ActionResult Index()
        {
            var rnd = new RndNumberGenerator();

            //int
            var randomNumber = rnd.GenerateInt(1, 10);

            //double
            var randomDouble = Math.Round(rnd.GenerateDouble(1, 10), 2);

            //bool
            var randomBool = rnd.GenerateInt(0, 1) == 0 ? false : true;

            //Guid
            var randomGuid = Guid.NewGuid();

            //Guid secuencial
            var randomGuidSequential = SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd);

            //Datetime I
            var year = rnd.GenerateInt(1970, 2025);
            var month = rnd.GenerateInt(1, 12);
            var day = rnd.GenerateInt(1, 28);
          
            var randomDate = new DateTime(year, month, day);

            //Datetime II
            var startDate = new DateTime(1970, 1, 1);
            var days = rnd.GenerateInt(1, 20000);
            var randomDate2 = startDate.AddDays(days);

            //TimeSpan
            var randomTimeSpan = randomDate - randomDate2;

            //string
            var randomString = new RandomStringHelper().Generate(50);
            var randomCode = new RandomStringHelper().GenerateCode();

            return View();
        }

        //public async Task<ActionResult> Generar(int cantidad = 100)
        //{
        //    var start = DateTime.Now;

        //    var cantidadElementosPorEjecucion = 100;
        //    var recorridasCompletas = Math.Floor(cantidad / Convert.ToDouble(cantidadElementosPorEjecucion));
        //    var cantidadRestante = Convert.ToInt32(Math.Floor(cantidad - 100 * recorridasCompletas));

        //    for (int recorrida = 0; recorrida < recorridasCompletas; recorrida++)
        //        await Ejecutar(cantidadElementosPorEjecucion);

        //    if (cantidadRestante > 0)
        //        await Ejecutar(cantidadRestante);

        //    var end = DateTime.Now;
        //    var span = (end - start).TotalMilliseconds;

        //    return Json(span + " ms para generar " + cantidad + " peliculas", JsonRequestBehavior.AllowGet);
        //}

        public async Task<bool> Ejecutar(int cantidad)
        {
            var db = new ApplicationDbContext();
            var rnd = new RndNumberGenerator();
            var rndString = new RandomStringHelper();

            //repositorios
            var generos = new Repositorio<Genero>(db).TraerTodos().Select(a => a.Id).ToList();
            var actores = new Repositorio<Actor>(db).TraerTodos().ToList();
            var cantidadActores = 6;

            for (int i = 0; i < cantidad; i++)
            {
                var pelicula = new Pelicula();
                pelicula.Actores = new List<Actor>();

                pelicula.Nombre = "pelicula" + i.ToString();

               // pelicula.Nombre = rndString.Generate(rnd.GenerateInt(5, 15));
                pelicula.Duracion = rnd.GenerateInt(400000, 3000000);

                var days = rnd.GenerateInt(1, 20000);
                pelicula.FechaEstreno= new DateTime(1970, 1, 1).AddDays(days);

                pelicula.GeneroId = generos[rnd.GenerateInt(0, generos.Count() - 1)];

                var cantidadActoresAGenerar = rnd.GenerateInt(1, cantidadActores);
                for (int j = 0; j < cantidadActoresAGenerar; j++)
                {
                    var ac = actores[rnd.GenerateInt(0, cantidadActores)];
                    pelicula.Actores.Add(ac);
                }

                db.Pelicula.Add(pelicula);
            }

            db.SaveChanges();
            return true;
        }

        public ActionResult ThreadExample(int cantidad = 100)
        {
            var start = DateTime.Now;

            Task.Run(() => Ejecutar(cantidad));


            var end = DateTime.Now;
            var span = (end - start).TotalMilliseconds;
            return Json(span + " ms para generar peliculas", JsonRequestBehavior.AllowGet);
        }



    }
}