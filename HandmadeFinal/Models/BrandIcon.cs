using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.Models
{
    public class BrandIcon
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(200)]
        public string Icon { get; set; }
        [Required]
        [NotMapped]
        public IFormFile Photo { get; set; }
        [NotMapped]
        public IFormFile UpdatePhoto { get; set; }
    }
}
