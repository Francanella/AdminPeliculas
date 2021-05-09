using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication2.Helpers;
using WebApplication2.Models;
using WebApplication2.Models.Dominios;
using WebApplication2.Models.DTOs;

namespace Website.Controllers
{
    public class RequestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task<ActionResult> GenerateActores()
        {
            var json = await APIHelper.GetActores();

            var actores = new JavaScriptSerializer().Deserialize<ApiActoresDTO>(json);

            foreach (var actor in actores.results)
            {
                var newActor = new Actor();
                newActor.Nombre = actor.name.first+" "+actor.name.last;

                db.Actors.Add(newActor);
            }
            db.SaveChanges();

            return Json(actores, JsonRequestBehavior.AllowGet);
        }
    }
}