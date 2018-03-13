using ActionFilter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActionFilter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult Contact()
        {
            String id = ViewBag.Id;
            ViewBag.Message = id;
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(String usuario, String password)
        {
            if (usuario == "PEPE" && password == "1234"|| usuario == "PEPA" && password == "4321")
            {
                //almacenamos el usuario en la sesion
                Session["USUARIO"] = usuario;
                return RedirectToAction("Tienda");
            }
            else
            {
                ViewBag.Mensaje = "Usuario/Password incorrecta";
                return View();
            }
        }
        [Filtro]
        public ActionResult Tienda()
        {
            String id = ViewBag.Id;
            ViewBag.Mensaje = id;
            return View();
        }
    }
}