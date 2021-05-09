using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class RestorePasswordByTokenVM
    {
        public Guid Token { get; set; }
        [Required(ErrorMessage = "Por favor complete la contraseña")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Por favor debe confirmar su contraseña")]
        public string ConfirmPassword { get; set; }
    }
}