using InduccionV7.Website.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Dominios
{
    [Table("Generos")]

    public class Genero : Entidad
    {
        public Guid? GeneroId { get; set; }
    }
}