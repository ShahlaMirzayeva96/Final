using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.ViewModels
{
    public class LoginView
    {
        [Required(ErrorMessage = "Zəhmət olmasa doldurun")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
