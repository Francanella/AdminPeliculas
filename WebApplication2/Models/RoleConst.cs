using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Flags]
    public enum RoleEnum
    {
        Administrador = 1,
        Suscriptor
    }

    public static class RoleConst
    {
        public const string Administrador = "Administrador";
        public const string Suscriptor = "Suscriptor";
    }
}