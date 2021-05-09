using InduccionV7.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Dominios
{
    public class Actor : Entidad
    {
        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }
}