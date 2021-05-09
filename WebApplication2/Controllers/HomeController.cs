using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Dominios;


namespace Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Seguridad
            bool esAdmin = false;
            bool esSuscriptor = false;
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var userID = User.Identity.GetUserId();
                esAdmin = User.IsInRole(RoleConst.Administrador);
                esSuscriptor = User.IsInRole(RoleConst.Suscriptor);
            }
            return View();
        }

            public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}