using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ActionFilter.Models;
using System.Diagnostics;

namespace ActionFilter.Filters
{
    public class FiltroAttribute : ActionFilterAttribute
    {
        ModeloUsuario modelo;
        ContextoPrueba contexto;
        public FiltroAttribute()
        {
            modelo = new ModeloUsuario();
            contexto = new ContextoPrueba();
        }
        

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            if (HttpContext.Current.Session["USUARIO"] == null)
            {
                // Si la información es nula, redireccionar a 
                // página de error u otra página deseada.
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                    { "Controller", "Home" },
                    { "Action", "Login" }
                });
            }
            else
            {
                String prueba =  HttpContext.Current.Session["USUARIO"].ToString();
                filterContext.Controller.ViewBag.Id = prueba;
            }
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            String U = filterContext.Controller.ViewBag.Mensaje;
            Usuario user = modelo.GetUsuario(U);           
            if (user!=null)
            {
                String Nombre = filterContext.Controller.ViewBag.Mensaje;
                String Fecha = DateTime.Now.ToLongDateString().ToString();
                String Controlador = filterContext.RouteData.Values["controller"].ToString() 
                       + filterContext.RouteData.Values["action"].ToString();
                int? Loging = user.Loging + 1;
                modelo.ModificarUsuario(Nombre,Fecha,Controlador,Loging.GetValueOrDefault());
            }
            else
            {
                Usuario usu = modelo.GetUsuarioId();     
                usu.Id = usu.Id+1;
                usu.Nombre = filterContext.Controller.ViewBag.Mensaje;
                usu.Fecha = DateTime.Now.ToLongDateString().ToString();
                usu.Controlador = filterContext.RouteData.Values["controller"].ToString()
                    + filterContext.RouteData.Values["action"].ToString();
                usu.Loging = 1;
                contexto.Usuario.Add(usu);
                contexto.SaveChanges();

            }
            base.OnActionExecuted(filterContext);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting", filterContext.RouteData);
            base.OnResultExecuting(filterContext);
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
            base.OnResultExecuted(filterContext);
        }
        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            Debug.WriteLine(message, "Action Filter Log");
        }
    }
}