using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.Models
{
    public class Logo
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Zəhmət olmasa doldurun")]
        public string LogoImage { get; set; }
    }
}