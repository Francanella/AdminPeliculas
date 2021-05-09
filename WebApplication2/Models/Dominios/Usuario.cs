using InduccionV7.Website.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Dominios
{
    [Table("Usuarios")]
    public class Usuario : Entidad
    {
        public Usuario()
        {

        }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public bool DebeCambiarContraseña { get; set; }
        public Guid? Token { get; set; }
    }
}