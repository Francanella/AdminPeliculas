using InduccionV7.Website.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Interfaces
{
    public interface IRepositorio<T> where T : class, IEntidad
    {
        T Traer(Guid? Id);
        IQueryable<T> TraerTodos(bool inclusiveEliminados);
        //Pagina<T> TraerPagina(int numeroPagina, int elementosPagina, string criterioOrdenamiento, bool ascendente, string criterioBusqueda);
        void Crear(T entidad, bool grabarCambios);
        void Modificar(T entidad, bool grabarCambios);
        void Eliminar(T entidad, bool grabarCambios);
        void GuardarCambios(T entidad);
    }
}