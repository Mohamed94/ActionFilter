using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActionFilter.Models
{
    public class ModeloUsuario
    {
        ContextoPrueba contexto;
        public ModeloUsuario()
        {
            contexto = new ContextoPrueba();
        }
        public Usuario GetUsuario(String Nombre)
        {
            var consulta = from datos in contexto.Usuario
                           where datos.Nombre == Nombre
                           select datos;
            return consulta.FirstOrDefault();
        }
        public Usuario GetUsuarioId()
        {
            var consulta = (from datos in contexto.Usuario
                            orderby datos.Id descending
                           select datos).Take(1);
            return consulta.FirstOrDefault();
        }
        public void ModificarUsuario(String Nombre,String Fecha,String Controlador,int Loging)
        {
            Usuario usu = GetUsuario(Nombre);
            usu.Fecha = Fecha;
            usu.Controlador = Controlador;
            usu.Loging = Loging;
            contexto.SaveChanges();

        }
    }
}