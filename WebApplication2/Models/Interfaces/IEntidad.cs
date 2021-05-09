using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InduccionV7.Website.Models.Interfaces
{
    public interface IEntidad
    {
        Guid Id { get; set; }
        string Nombre { get; set; }
        DateTime FechaCreacion { get; set; }
        DateTime FechaEdicion { get; set; }
        bool Eliminado { get; set; }

    }
}
