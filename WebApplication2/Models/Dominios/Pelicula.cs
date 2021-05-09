using InduccionV7.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Dominios
{
    public class Pelicula : Entidad
    {
        public DateTime? FechaEstreno { get; set; }
        public int Duracion { get; set; }
        public virtual Genero Genero { get; set; }
        public Guid? GeneroId { get; set; }
        public virtual ICollection<Actor> Actores { get; set; }
        public string URLImage { get; set; }
        public string Sinopsis { get; set; }
    }
}